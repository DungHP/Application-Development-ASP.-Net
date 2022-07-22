using System;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Enums;

namespace WebApplication2.Models
{
  public class Todo
  {
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(255)]
    public string Description   { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public TodoStatus Status { get; set; } = TodoStatus.Todo;
  }
}
