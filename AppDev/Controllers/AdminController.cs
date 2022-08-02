using AppDev.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppDev.Controllers
{
  [Authorize(Roles = Role.ADMIN)]
  public class AdminController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
