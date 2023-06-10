using Microsoft.AspNetCore.Mvc;

namespace Veterinaria.Controllers
{
    public class VeterinariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
