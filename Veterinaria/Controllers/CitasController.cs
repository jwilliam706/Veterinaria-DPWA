using Microsoft.AspNetCore.Mvc;

namespace Veterinaria.Controllers
{
    public class CitasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
