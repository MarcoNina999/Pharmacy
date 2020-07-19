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

        [StringLength(50)]
        //public string Id_Cliente { get; set; }
        //[StringLength(50)]
        public string Description { get; set; }
        public int Detail { get; set; }
        [StringLength(200)]
        public int Price { get; set; }
        [StringLength(200)]
        public DateTime Date { get; set; }
        public byte[] Image { get; set; }
       
        public int ProdId { get; set; } //obtener la id de la otra tabla

        [ForeignKey("ProdId")]
        public Product Product { get; set; }
    }
}