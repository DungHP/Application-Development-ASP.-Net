using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using WebApplication2.Models;

namespace AppDev.Models
{
  public class ApplicationUser: IdentityUser
  {
    public string FullName { get; set; }
    public string Address { get; set; }
    
    public List<Todo> Todoes { get; set; }
  }
}
