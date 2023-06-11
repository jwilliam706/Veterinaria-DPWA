using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
    public class VeterinariosController : Controller
    {

        VeterinariaContext contexto = new VeterinariaContext();
        public IActionResult Index()
        {
            var veterinarios = contexto.Veterinarios.ToList();

            return View(veterinarios);
           
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Nombre,Telefono,Sexo,Direccion")] Veterinario veterinario)
        {
            if (ModelState.IsValid)
            {
                contexto.Add(veterinario);
                contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(veterinario);
        }

        public  IActionResult Edit(int? id)
        {
            if (id == null || contexto.Veterinarios == null)
            {
                return NotFound();
            }

            var veterinario = contexto.Veterinarios.Find(id);
            if (veterinario == null)
            {
                return NotFound();
            }
            return View(veterinario);
        
        
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Nombre,Telefono,Sexo,Direccion")] Veterinario veterinario)
        {
            if (id != veterinario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contexto.Update(veterinario);
                    contexto.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinarioExists(veterinario.Id))
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
            return View(veterinario);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || contexto.Veterinarios == null)
            {
                return NotFound();
            }

            var veterinario = contexto.Veterinarios
                .FirstOrDefault(m => m.Id == id);
            if (veterinario == null)
            {
                return NotFound();
            }

            return View(veterinario);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (contexto.Veterinarios == null)
            {
                return Problem("Entity set 'VeterinariaContext.Veterinarios'  is null.");
            }
            var veterinario = contexto.Veterinarios.Find(id);
            if (veterinario != null)
            {
                contexto.Veterinarios.Remove(veterinario);
            }

            contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        private bool VeterinarioExists(int id)
        {
            return (contexto.Veterinarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
