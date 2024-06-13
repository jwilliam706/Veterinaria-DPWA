using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Veterinaria.Models;
using BCrypt.Net;

namespace Veterinaria.Controllers
{
	public class UsuariosController : Controller
    {
        VeterinariaContext contexto = new VeterinariaContext();
       
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var usuarios = contexto.Usuarios.ToList();
            
            return View(usuarios);
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
        }

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Usuario1,Password,Role")] Usuario usuario)
		{
			if (ModelState.IsValid)
			{
				usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
				contexto.Add(usuario);
				contexto.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			return View(usuario);
		}

		public IActionResult Edit(int? id)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || contexto.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = contexto.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Usuario1","Password","Role")] Usuario usuario)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
                    contexto.Update(usuario);
                    contexto.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        public IActionResult Delete(int? id)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || contexto.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = contexto.Usuarios
                .FirstOrDefault(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (contexto.Usuarios == null)
            {
                return Problem("Entity set 'VeterinariaContext.Usuarios'  is null.");
            }
            var usuario = contexto.Usuarios.Find(id);
            if (usuario != null)
            {
                contexto.Usuarios.Remove(usuario);
            }
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
           
            return (contexto.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
