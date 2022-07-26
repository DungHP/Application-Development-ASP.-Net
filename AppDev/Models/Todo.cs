using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Enums;

namespace WebApplication2.Models
{
  public class Todo
  {
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "You haven't input anything in the description!")]
    [StringLength(255)]
    public string Description   { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public TodoStatus Status { get; set; } = TodoStatus.Todo;

    [Required]
    [ForeignKey("Category")]
    public int CategoryId { get; set; } 
    public Category Category { get; set; }
    public string UserId { get; set; }
    public IdentityUser User { get; set; }  
  }
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
