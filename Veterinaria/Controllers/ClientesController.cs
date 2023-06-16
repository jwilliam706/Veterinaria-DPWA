using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
    public class ClientesController : Controller
    {

        VeterinariaContext contexto = new VeterinariaContext();
        public IActionResult Index()
        {
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			var clientes = contexto.Clientes.ToList();
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            
            return View(clientes);
        }

        public IActionResult Create()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nombre,Telefono,Sexo,Direccion")] Cliente cliente)
        {
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			if (ModelState.IsValid)
            {
                contexto.Add(cliente);
                contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(cliente);
        }



        public IActionResult Edit(int? id)
        {
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			if (id == null || contexto.Clientes == null)
            {
                return NotFound();
            }

            var cliente = contexto.Clientes.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(cliente);
        }

        [HttpPost]
        public  IActionResult Edit(int id, [Bind("Id,Nombre,Telefono,Sexo,Direccion")] Cliente cliente)
        {
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contexto.Update(cliente);
                    contexto.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(cliente);
        }

        public IActionResult Delete(int? id)
        {
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			if (id == null || contexto.Clientes == null)
            {
                return NotFound();
            }

            var cliente = contexto.Clientes
                .FirstOrDefault(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			if (contexto.Clientes == null)
            {
                return Problem("Entity set 'VeterinariaContext.Clientes'  is null.");
            }
            var cliente = contexto.Clientes.Find(id);
            if (cliente != null)
            {
                contexto.Clientes.Remove(cliente);
            }
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool ClienteExists(int id)
        {
            return (contexto.Clientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
