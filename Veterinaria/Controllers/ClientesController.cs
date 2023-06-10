using Microsoft.AspNetCore.Mvc;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
             var contexto = new VeterinariaContext();
            var clientes = contexto.Clientes.ToList();
            ViewBag.clientes = clientes;
            return View();
        }
    }
}
