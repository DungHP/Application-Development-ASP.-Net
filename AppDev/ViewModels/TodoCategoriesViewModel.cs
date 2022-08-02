using AppDev.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using WebApplication2.Models;

namespace AppDev.ViewModels
{
  public class TodoCategoriesViewModel
{
    public Todo Todo { get; set; }
    public IEnumerable<Category> Categories { get; set; }

    [Display(Name = "File")]
    public IFormFile FormFile { get; set; }
  }
}