using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Linq;
using System.Security.Policy;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
 

  public class TodoesController : Controller
  {
    private ApplicationDbContext _context;
    public TodoesController(ApplicationDbContext context)
    {
      _context = context;
    }
    [HttpGet]
    public IActionResult Index()
    {
      IEnumerable todoes = _context.Todoes.ToList();
      return View(todoes);
    }

    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public IActionResult Create(Todo todo)
    {
      var newTodo = new Todo
      {
        Description = todo.Description
      };

      _context.Add(newTodo);
      _context.SaveChanges();
      return RedirectToAction("Index");
      
    }
    [HttpGet]
    public IActionResult Delete(int id)
    {
      var todoInDb = _context.Todoes.SingleOrDefault(t => t.Id == id);
      if (todoInDb is null)
      {
        return NotFound();
      }

      _context.Todoes.Remove(todoInDb);
      _context.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
      var todoInDb = _context.Todoes.SingleOrDefault(t => t.Id == id);
      if (todoInDb is null)
      {
        return NotFound();
      }

      return View(todoInDb);
    }

    [HttpPost]
    public IActionResult Edit(Todo task)
    {
      var todoInDb = _context.Todoes.SingleOrDefault(t => t.Id == task.Id);
      if(todoInDb is null)
      {
        return BadRequest();
      }

      todoInDb.Description = task.Description;
      todoInDb.Status = task.Status;

      _context.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Detail(int id)
    {
      var todoInDb = _context.Todoes.SingleOrDefault(t => t.Id == id);
      if (todoInDb is null)
      {
        return NotFound();
      }

      return View(todoInDb);
    }
  }
}
