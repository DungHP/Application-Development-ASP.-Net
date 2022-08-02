using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;

namespace AppDev.Models
{
  public class Category
  {
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Description can't be empty!")]
    [StringLength(255)]
    public string Description { get; set; }
    public List<Todo> Todoes { get; set; }
  }
}
