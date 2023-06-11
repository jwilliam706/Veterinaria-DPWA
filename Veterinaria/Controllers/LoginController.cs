using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
	public class LoginController : Controller
	{

		private readonly VeterinariaContext _context;

		

		public LoginController(VeterinariaContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{

			return View();
		}

		[HttpPost]
		public IActionResult Index(string Usuario1, string Password)
		{
			var user = _context.Usuarios.SingleOrDefault(u => u.Usuario1 == Usuario1 && u.Password == Password);
			if (user == null)
			{
				ModelState.AddModelError("", "Invalid username or password");
				ViewData["Error"] = "Usuario o contraseña incorrecta";
				return View();
			}
			else if (user.Role == "Administrador")
			{
				HttpContext.Session.SetString("UserName", user.Usuario1);
				ViewBag.sesion = HttpContext.Session.GetString("UserName");
				return RedirectToAction("Index", "Home");

			}
			else
			{
				HttpContext.Session.SetString("UserName", user.Usuario1);
				ViewBag.sesion = HttpContext.Session.GetString("UserName");
				return RedirectToAction("Privacy", "Home");
			}
			
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Login");

		}
	}
}
