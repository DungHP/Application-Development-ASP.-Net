using AppDev.Models;
using System.Collections.Generic;

namespace AppDev.Repositories.Interfaces
{
  public interface ICategoryRepository
  {
    Category GetById(int id);
    IEnumerable<Category> GetAll();
  }
}
