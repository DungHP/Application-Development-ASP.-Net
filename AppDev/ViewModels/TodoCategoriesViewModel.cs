using System.Collections.Generic;
using WebApplication2.Models;

namespace AppDev.ViewModels
{
  public class TodoCategoriesViewModel
  {
    public Todo Todo { get; set; }
    public IEnumerable<Category> Categories { get; set; }
  }
}