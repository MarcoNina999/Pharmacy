using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class SaleDetails
    {
        [Key]
        public int Id { get; set; }
        //[Display(Name = "NumFactura")]
        public int numBill { get; set; }
        [ForeignKey("numBill")]
        public Bill Bill { get; set; }
        public float SubTotal { get; set; }
        [Display(Name = "Descuento")]
        [Column(TypeName = "money")]
        public int Discount { get; set; }
        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }

        public int SaleId { get; set; }
        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}