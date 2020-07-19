using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class BookingsDetails
    {
        [Key]
        public int Id { get; set; }        
        public float SubTotal { get; set; }
        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }
        public int BookingId { get; set; }
        [ForeignKey("BookingId")]
        public Bookings Bookings { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}