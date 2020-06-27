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
        public string Name { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        [StringLength(200)]
        public string Symptoms { get; set; }
        public byte[] Image { get; set; }
        public System.DateTime Create_at { get; set; }
        public bool Is_active { get; set; }
    }
}