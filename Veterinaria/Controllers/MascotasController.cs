using Microsoft.AspNetCore.Mvc;

namespace Veterinaria.Controllers
{
    public class MascotasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detalle(int id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}
