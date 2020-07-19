using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy.Services
{
    public class ReservaRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<Reserva> ObtenerTodos()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Reservas.ToList();
            }
        }

        internal void Crear(Reserva model)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Reservas.Add(model);
                db.SaveChanges();
            }
        }
        internal void Eliminar(int id)
        {
            using (var _db = new ApplicationDbContext())
            {
                var model = _db.Reservas.Where(x => x.Id == id).FirstOrDefault();
                _db.Reservas.Remove(model);
                _db.SaveChanges();
            }
        }

        internal void Modificar(Reserva model)
        {
            using (var _db = new ApplicationDbContext())
            {
                _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }
        }
        public int ObtenerCantReservados()
        {
            //o
            int CantReservados=4;
            return CantReservados;


        }
        public int ObtenerCantActualProd()
        {
            //o
            int CantProd = 4;
            return CantProd;
        }
        
    }
}
