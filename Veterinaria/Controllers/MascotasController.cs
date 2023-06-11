using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
    public class MascotasController : Controller
    {
		VeterinariaContext contexto = new VeterinariaContext();
		public IActionResult Index()
		{
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			var mascotas = contexto.Mascotas.ToList();

			return View(mascotas);
		}

		public IActionResult Create()
		{
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
            ViewBag.Clientes = contexto.Clientes.ToList();
            return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Nombre,Tipo,Sexo,FechaNacimiento,ClienteId")] Mascota mascota)
		{
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			if (ModelState.IsValid)
			{
				contexto.Add(mascota);
				contexto.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			return View(mascota);
		}



		public IActionResult Edit(int? id)
		{
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
            ViewBag.Clientes = contexto.Clientes.ToList();
            if (id == null || contexto.Mascotas == null)
			{
				return NotFound();
			}

			var mascota = contexto.Mascotas.Find(id);
			if (mascota == null)
			{
				return NotFound();
			}
			return View(mascota);
		}

		[HttpPost]
		public IActionResult Edit(int id, [Bind("Id,Nombre,Tipo,Sexo,FechaNacimiento,ClienteId")] Mascota mascota)
		{
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			if (id != mascota.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					contexto.Update(mascota);
					contexto.SaveChanges();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!MascotaExists(mascota.Id))
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
			return View(mascota);
		}

		public IActionResult Delete(int? id)
		{
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			if (id == null || contexto.Mascotas == null)
			{
				return NotFound();
			}

			var mascota = contexto.Mascotas
				.FirstOrDefault(m => m.Id == id);
			if (mascota == null)
			{
				return NotFound();
			}

			return View(mascota);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			if (contexto.Mascotas == null)
			{
				return Problem("Entity set 'VeterinariaContext.Mascotas'  is null.");
			}
			var mascota = contexto.Mascotas.Find(id);
			if (mascota != null)
			{
				contexto.Mascotas.Remove(mascota);
			}

			contexto.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}



		private bool MascotaExists(int id)
		{
			return (contexto.Mascotas?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
