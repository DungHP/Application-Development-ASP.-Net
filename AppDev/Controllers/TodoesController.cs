using AppDev.DTOs.Requests;
using AppDev.DTOs.Responses;
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
using System.Runtime.CompilerServices;
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
        var result = _todoRepos.GetAll();


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
    public async Task<IActionResult> Create(CreateTodoRequest model)
    {
      if (!ModelState.IsValid)
      {
        var viewModel = new CreateTodoRequest()
        {
          Categories = _categoryRepos.GetAll()
        };
        return View(viewModel);
      }
      var currentUserId = _userManager.GetUserId(User);

      bool isCreated = await _todoRepos.CreateTodo(model, currentUserId); ;

      if (!isCreated) return BadRequest();
      return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Delete(int id)
    {
      var currentUserId = _userManager.GetUserId(User);
      var isDeleted = _todoRepos.DeleteTodo(id, currentUserId);

      if (!isDeleted) return NotFound();
      return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
      var currentUserId = _userManager.GetUserId(User);
      var todoInDb = _todoRepos.GetTodo(id, currentUserId);
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
    public IActionResult Edit(EditTodoRequest model)
    {
      
      if (!ModelState.IsValid)
      {
        model = new EditTodoRequest
        {
          Id = model.Id,
          Description = model.Description,
          CategoryId = model.CategoryId,
          Status = model.Status,
          Categories = _categoryRepos.GetAll()
        };
        return View(model);
      }

      var currentUserId = _userManager.GetUserId(User);
      var isEdited = _todoRepos.EditTodo(model, currentUserId);

      _context.SaveChanges();

      if (!isEdited) return BadRequest();
      return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Detail(int id)
    {
      var currentUserId = _userManager.GetUserId(User);
      var todoInDb = _todoRepos.GetTodo(id, currentUserId);
      if (todoInDb is null)
      {
        return NotFound();
      }
      var response = new DetailsTodoResponse
      {
        Description = todoInDb.Description,
        CreatedAt = todoInDb.CreatedAt,
        Status = todoInDb.Status,
        CategoryDescription = todoInDb.Category.Description,
        ImageUrl = ConvertByteArrayToStringBase64(todoInDb.ImageData)
      };
    

      return View(response);
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
      var todoesByCategoryName = GetTodoesFromCategoryAndUserId(categoryInDb, currentUserId);
      return View("Index", todoesByCategoryName);
    }
    [NonAction]
    private List<Todo> GetTodoesFromCategoryAndUserId(Category category, string userId)
    {
      return category.Todoes
        .Where(t => t.UserId == userId)
        .ToList();
    }
    [NonAction]
    private string ConvertByteArrayToStringBase64(byte[] imageArray)
    {
      string imageBase64Data = Convert.ToBase64String(imageArray);

      return string.Format("data:image/jpg;base64, {0}", imageBase64Data);
    }
  }
}
