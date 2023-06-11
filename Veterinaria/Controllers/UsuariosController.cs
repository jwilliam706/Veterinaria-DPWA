using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Sockets;
using Veterinaria.Models;

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
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Usuario1,Password,Role")] Usuario usuario)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
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

            contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool UsuarioExists(int id)
        {
           
            return (contexto.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
