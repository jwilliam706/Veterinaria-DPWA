﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
    public class VeterinariosController : Controller
    {

        VeterinariaContext contexto = new VeterinariaContext();
        public IActionResult Index()
        {
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			var veterinarios = contexto.Veterinarios.ToList();
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(veterinarios);
           
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
        public ActionResult Create([Bind("Id,Nombre,Telefono,Sexo,Direccion")] Veterinario veterinario)
        {
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			if (ModelState.IsValid)
            {
                contexto.Add(veterinario);
                contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(veterinario);
        }

        public  IActionResult Edit(int? id)
        {
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			if (id == null || contexto.Veterinarios == null)
            {
                return NotFound();
            }

            var veterinario = contexto.Veterinarios.Find(id);
            if (veterinario == null)
            {
                return NotFound();
            }
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(veterinario);
        
        
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Nombre,Telefono,Sexo,Direccion")] Veterinario veterinario)
        {
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
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
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(veterinario);
        }

        public IActionResult Delete(int? id)
        {
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
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
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return View(veterinario);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return RedirectToAction("Index", "Login");
			}
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
            string username = HttpContext.Session.GetString("UserName");
            string role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role;
            ViewBag.Username = username;
            return RedirectToAction(nameof(Index));
        }


        private bool VeterinarioExists(int id)
        {
            return (contexto.Veterinarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
