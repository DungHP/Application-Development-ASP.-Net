using AppDev.Models;
using System.Collections.Generic;
using WebApplication2.Enums;

namespace AppDev.DTOs.Requests
{
  public class EditTodoRequest
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public TodoStatus Status { get; set; }
    public int CategoryId { get; set; }
    public IEnumerable<Category> Categories { get; set; }
  }
}