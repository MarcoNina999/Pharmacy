using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [StringLength(50)]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }
        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }
        [Display(Name = "Precio")]
        public int Price { get; set; }
        [StringLength(200)]
        [Display(Name = "Sintomas")]
        public string Symptoms { get; set; }        
        [Display(Name = "Imagen")]
        public byte[] Image { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Creacion")]
        public System.DateTime Create_at { get; set; }
        [Display(Name = "¿Esta activo?")]
        public bool Is_active { get; set; }
    }
}