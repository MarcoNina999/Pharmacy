using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        

        public float Pricio { get; set; }  
        public int CatReservas { get; set; }
        public DateTime Fecha { get; set; }
        //public int UserId { get; set; }
        //ForeignKey("UserId")]
        //public  UserId { get; set; }

        public int ProdId { get; set; } //obtener la id de la otra tabla

        [ForeignKey("ProdId")]
        public Product Product { get; set; }
    }
}