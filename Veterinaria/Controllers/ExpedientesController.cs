using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
    public class ExpedientesController : Controller
    {
        VeterinariaContext contexto = new VeterinariaContext();
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var expedientes = contexto.Expedientes.ToList();
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(expedientes);
        }

        public IActionResult Create()
        {
           
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Mascotas = contexto.Mascotas.ToList();
            ViewBag.Citas = contexto.Citas.ToList();
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( [Bind("Id,MascotaId,CitaId,Diagnostico,Recetas")] Expediente expediente)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            
        
            if (ModelState.IsValid)
            {

                
                contexto.Add(expediente);
                contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(expediente);
        }



        public IActionResult Edit(int? id)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Citas = contexto.Citas.ToList();
            ViewBag.Mascotas = contexto.Mascotas.ToList();
            if (id == null || contexto.Expedientes == null)
            {
                return NotFound();
            }

            var expediente = contexto.Expedientes.Find(id);
            if (expediente == null)
            {
                return NotFound();
            }
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(expediente);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,MascotaId,CitaId,Diagnostico,Recetas")] Expediente expediente)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != expediente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contexto.Update(expediente);
                    contexto.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpedienteExists(expediente.Id))
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
            return View(expediente);
        }

        public IActionResult Delete(int? id)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || contexto.Expedientes == null)
            {
                return NotFound();
            }

            var expediente = contexto.Expedientes
                .FirstOrDefault(m => m.Id == id);
            if (expediente == null)
            {
                return NotFound();
            }
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(expediente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (contexto.Expedientes == null)
            {
                return Problem("Entity set 'VeterinariaContext.Mascotas'  is null.");
            }
            var expediente = contexto.Expedientes.Find(id);
            if (expediente != null)
            {
                contexto.Expedientes.Remove(expediente);
            }

            contexto.SaveChangesAsync();
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return RedirectToAction(nameof(Index));
        }



        private bool ExpedienteExists(int id)
        {
            return (contexto.Expedientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
