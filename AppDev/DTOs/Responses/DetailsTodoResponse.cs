using System;
using WebApplication2.Enums;

namespace AppDev.DTOs.Responses
{
  public class DetailsTodoResponse
  {
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public TodoStatus Status { get; set; }
    public string CategoryDescription { get; set; }
    public string ImageUrl { get; set; }
  }
}
