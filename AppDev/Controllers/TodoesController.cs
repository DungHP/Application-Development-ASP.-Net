using AppDev.Models;
using AppDev.Repositories.Interfaces;
using AppDev.Utils;
using AppDev.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
   [Authorize(Roles = Role.USER)]
  public class TodoesController : Controller
  {
    private ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private ITodoRepository _todoRepos;
    private ICategoryRepository _categoryRepos;
    public TodoesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ITodoRepository todoRepos, ICategoryRepository categoryRepos)
    {
      _context = context;
      _userManager = userManager;
      _todoRepos = todoRepos;
      _categoryRepos = categoryRepos;
    }

    [HttpGet]
    public IActionResult Index(string category)
    {
      var currentUserId = _userManager.GetUserId(User);
      if (!string.IsNullOrWhiteSpace(category))
      {
        /*  var result = _context.Todoes
            .Include(t => t.Category)
            .Where(t => t.Category.Description.Equals(category)
                && t.UserId == currentUserId
            )
            .ToList();*/
        var result = _todoRepos.GetAll().Where(t => t.Category.Description.Equals(category) && t.UserId == currentUserId);

        
        return View(result);
      }

      IEnumerable<Todo> todoes = _context.Todoes
        .Include(t => t.Category)
        .Where(t => t.UserId == currentUserId)
        .ToList();
      return View(todoes);
    }

    [HttpGet]
    public IActionResult Create()
    {
      var viewModel = new TodoCategoriesViewModel()
      {
        Categories = _categoryRepos.GetAll()
      };
      return View(viewModel);
    }
    [HttpPost]
    public async Task<IActionResult> Create(TodoCategoriesViewModel viewModel)
    {
      if (!ModelState.IsValid)
      {
        viewModel = new TodoCategoriesViewModel
        {
          Categories = _categoryRepos.GetAll()
        };
        return View(viewModel);
      }
      var currentUserId = _userManager.GetUserId(User);

      bool isCreated = await _todoRepos.CreateTodoWithUserId(viewModel, currentUserId);

      if (!isCreated) return BadRequest();
      return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Delete(int id)
    {
      var currentUserId = _userManager.GetUserId(User);
      var isDeleted = _todoRepos.DeleteByIdAndUserId(id, currentUserId);

      if (!isDeleted) return NotFound();
      return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
      var currentUserId = _userManager.GetUserId(User);
      var todoInDb = _todoRepos.GetByTodoIdAndUserId(id, currentUserId);
      if (todoInDb is null)
      {
        return NotFound();
      }
      var viewModel = new TodoCategoriesViewModel
      {
        Todo = todoInDb,
        Categories = _categoryRepos.GetAll()
      };
      return View(viewModel);
    }

    [HttpPost]
    public IActionResult Edit(TodoCategoriesViewModel viewModel)
    {
   
      if (!ModelState.IsValid)
      {
        viewModel = new TodoCategoriesViewModel
        {
          Todo = viewModel.Todo,
          Categories = _categoryRepos.GetAll()
        };
        return View(viewModel);
      }

      var currentUserId = _userManager.GetUserId(User);
      var isEdited = _todoRepos.EditTodo(viewModel, currentUserId);

      _context.SaveChanges();

      if (!isEdited) return BadRequest();
      return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Detail(int id)
    {
      var currentUserId = _userManager.GetUserId(User);
      var todoInDb = _todoRepos.GetByTodoIdAndUserId(id, currentUserId);
      if (todoInDb is null)
      {
        return NotFound();
      }
      string imageBase64Data = Convert.ToBase64String(todoInDb.ImageData);
      string image = string.Format("data:image/jpg;base64, {0}", imageBase64Data);
      ViewBag.ImageData = image;

      return View(todoInDb);
    }

    [HttpGet]
    public IActionResult ByCategory(int id)
    {
      var categoryInDb = _categoryRepos.GetById(id);

      if (categoryInDb == null)
      {
        return NotFound();
      }
      var currentUserId = _userManager.GetUserId(User);
      var todoesByCategoryName = categoryInDb.Todoes
        .Where(t => t.UserId == currentUserId)
        .ToList();
      return View("Index", todoesByCategoryName);
    }

   
  }
}
