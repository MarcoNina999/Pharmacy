using Pharmacy.Models;
using Pharmacy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy.Controllers
{
    public class ReservasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ReservaRepository _db;

        // GET: Reservas
        public ReservasController()
        {
            _db = new ReservaRepository();
        }
        public ActionResult Index()
        {
            var model = _db.ObtenerTodos();
            return View(model);
            // return View(db.Reservas.ToList());
        }

        // GET: Reservas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reservas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reservas/Create
        [HttpPost]
        public ActionResult Create(Reserva model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //el metodo crear lo generamos y este se genero en el Reposytory
                    _db.Crear(model); //queremos crear  y mandamos los datos o modelo
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                //return View();
            }
            return View(model);
        }

        // GET: Reservas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
            //return View();
        }

        // POST: Reservas/Edit/5
        [HttpPost]
        public ActionResult Edit(Reserva model, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //el metodo crear lo generamos y este se genero en el Reposytory
                    _db.Modificar(model); //queremos crear  y mandamos los datos o modelo
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                //return View();
            }
            return View(model);
        }


        // GET: Reservas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reservas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //el metodo crear lo generamos y este se genero en el Reposytory
                    _db.Eliminar(id); //queremos crear  y mandamos los datos o modelo
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                //return View();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Validacion(variables c, string nombre)
        {

            int T = _db.ObtenerCantReservados();
            int A = _db.ObtenerCantActualProd();
            int B = c.CantAReservar + T;
            int C = A - B;
            if (C < 0)
            {
                //no se borran las variables sesion
                return RedirectToAction("Index");
            }
            else
            {
                //se borran las variables sesion
                return View("compra exitosa");
            }

        }
    }
}
   

