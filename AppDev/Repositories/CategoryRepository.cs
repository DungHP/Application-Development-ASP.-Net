using AppDev.Models;
using AppDev.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using WebApplication2.Data;

namespace AppDev.Repositories
{
  public class CategoryRepository : ICategoryRepository
  {
    ApplicationDbContext _context;
   public CategoryRepository(ApplicationDbContext context)
    {
      context = _context;
    }

    public IEnumerable<Category> GetAll()
    {
      return _context.Categories.ToList();
    }

    public Category GetById(int id)
    {
      return _context.Categories
       .Include(t => t.Todoes)
       .SingleOrDefault(t => t.Id == id);
    }
  }
}
