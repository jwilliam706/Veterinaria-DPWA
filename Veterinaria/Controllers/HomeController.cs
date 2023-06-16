using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
    
    public class HomeController : Controller
    {
       
        private readonly ILogger<HomeController> _logger;

        VeterinariaContext _context=new VeterinariaContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
           
        }

        public IActionResult Index()
        {
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
            
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            
            return View();
        }

        public IActionResult Privacy()
        {
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}