using Microsoft.AspNetCore.Mvc;

namespace Veterinaria.Controllers
{
    public class CitasController : Controller
    {
        public IActionResult Index()
        {
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			return View();
        }
    }
}
