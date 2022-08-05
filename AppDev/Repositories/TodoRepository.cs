using AppDev.Repositories.Interfaces;
using AppDev.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;

namespace AppDev.Repositories
{
  public class TodoRepository : ITodoRepository
  {
    private ApplicationDbContext _context;
    public TodoRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public IEnumerable<Todo> GetAll()
    {
      return _context.Todoes
        .Include(t => t.Category)
        .ToList();
    }
    public bool CreateTodo(TodoCategoriesViewModel viewModel)
    {
      throw new System.NotImplementedException();
    }

    public bool DeleteById(int id)
    {
      throw new System.NotImplementedException();
    }

    public bool EditTodo(TodoCategoriesViewModel viewModel, string userId)
    {
      var todoInDb = GetByTodoIdAndUserId(viewModel.Todo.Id, userId);
      if (todoInDb == null) return false;

      todoInDb.Description = viewModel.Todo.Description;
      todoInDb.Status = viewModel.Todo.Status;
      todoInDb.CategoryId = viewModel.Todo.CategoryId;

      return _context.SaveChanges() > 0;
    }


    public Todo GetById(int id)
    {
      return _context.Todoes
        .Include(t => t.Category)
        .SingleOrDefault(t => t.Id == id);
    }

    public IEnumerable<Todo> GetTodesByCategoryId(int id)
    {
      throw new System.NotImplementedException();
    }

    public Todo GetByTodoIdAndUserId(int id, string userId)
    {
      return _context.Todoes
        .Include(t => t.Category)
        .SingleOrDefault(t => t.Id == id && t.UserId == userId);
    }

    public bool DeleteByIdAndUserId(int id, string userId)
    {
      var todoInDb = GetByTodoIdAndUserId(id, userId);
      if (todoInDb == null) return false;

      _context.Todoes.Remove(todoInDb);
      _context.SaveChanges();
      return true;
    }

    public async Task<bool> CreateTodoWithUserId(TodoCategoriesViewModel viewModel, string userId)
    {
      int result;
      using (var memoryStream = new MemoryStream())
      {
        await viewModel.FormFile.CopyToAsync(memoryStream);
        var newTodo = new Todo
        {
          Description = viewModel.Todo.Description,
          CategoryId = viewModel.Todo.CategoryId,
          UserId = userId,
          ImageData = memoryStream.ToArray()
        };
        _context.Add(newTodo);
        result = await _context.SaveChangesAsync();
      }
      return result > 0;
    }

    public bool EditTodo(TodoCategoriesViewModel viewModel)
    {
      throw new System.NotImplementedException();
    }
  }
}
