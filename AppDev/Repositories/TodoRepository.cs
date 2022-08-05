using AppDev.Repositories.Interfaces;
using AppDev.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

    public bool EditTodo(TodoCategoriesViewModel viewModel)
    {
      throw new System.NotImplementedException();
    }


    public Todo GetById(int id)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Todo> GetTooesByCategoryId(int id)
    {
      throw new System.NotImplementedException();
    }
  }
}
