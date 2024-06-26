﻿using Microsoft.AspNetCore.Mvc;
using Veterinaria.Models;
using Microsoft.EntityFrameworkCore;

namespace Veterinaria.Controllers
{
    public class CitasController : Controller
    {
        VeterinariaContext contexto = new VeterinariaContext();
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var citas = contexto.Citas.Include(cita => cita.Mascota).Include(cita => cita.Veterinario).ToList();
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(citas);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
            var mascotas = contexto.Mascotas.Include(mascota => mascota.Cliente).ToList();
            ViewBag.mascotas = mascotas;
            ViewBag.veterinarios = contexto.Veterinarios.ToList();
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Fecha,Hora,MascotaId,VeterinarioId")] Cita cita)
        {
            if(HttpContext.Session.GetString("UserName") == null)

            {
                return RedirectToAction("Index", "Login");
            }
            cita.Estado = "Pendiente";
            if (ModelState.IsValid)
            {
                contexto.Add(cita);
                contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(cita);
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

            var cita = contexto.Citas.Find(id);
            if (cita == null)
            {
                return NotFound();
            }
            var mascotas = contexto.Mascotas.Include(mascota => mascota.Cliente).ToList();
            ViewBag.mascotas = mascotas;
            ViewBag.veterinarios = contexto.Veterinarios.ToList();
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(cita);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Fecha,Hora,MascotaId,VeterinarioId, Estado")] Cita cita)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != cita.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contexto.Update(cita);
                    contexto.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.Id))
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
            return View(cita);
        }

        public IActionResult Delete(int? id)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || contexto.Citas == null)
            {
                return NotFound();
            }

            var cita = contexto.Citas
                .FirstOrDefault(m => m.Id == id);
            if (cita == null)
            {
                return NotFound();
            }
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;

            return View(cita);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (contexto.Citas == null)
            {
                return Problem("Entity set 'VeterinariaContext.Clientes'  is null.");
            }
            var cita = contexto.Citas.Find(id);
            if (cita != null)
            {
                contexto.Citas.Remove(cita);
            }

            contexto.SaveChangesAsync();
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return RedirectToAction(nameof(Index));

        }

        private bool CitaExists(int id)
        {
            return (contexto.Citas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
