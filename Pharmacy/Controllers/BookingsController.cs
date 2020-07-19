using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using PagedList;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace Pharmacy.Controllers
{
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        [HttpGet]
        public ActionResult NewBooking(int? id)
        {
            carga(id);
            return View();
        }
        public ActionResult GuardarReserva(string Fecha, string modoPago, string IdCliente, string Total, List<SaleDetails> ListadoDetalle)
        {
            Random random = new Random();
            string mensaje = "";
            double iva = 49;
            string idVendedor = "321";
            int codigoPago = 0;
            string codigoCliente = "";
            double total = 0;

            if (Fecha == ""/* || modoPago == "" || IdCliente == "" || Total == ""*/)
            {
                if (Fecha == "") mensaje = "ERROR EN EL CAMPO FECHA";
                if (modoPago == "") mensaje = "SELECCIONE UN MODO DE PAGO";
                if (IdCliente == "") mensaje = "ERROR CON EL CODIGO DEL CLIENTE";
                if (Total == "") mensaje = "ERROR EN EL CAMPO TOTAL";
            }
            else
            {
                int SaleId = random.Next(999999);
                //codigoPago = Convert.ToInt32(modoPago);
                //codigoCliente = Convert.ToInt64(IdCliente);
                total = Convert.ToDouble(Total);

                //SALE REGISTER
                db.Database.ExecuteSqlCommand(ResourceSQL.CreateBookingName + "@id, @total, @Create_at, @UserId", new SqlParameter("@id", SaleId), new SqlParameter("@total", total), new SqlParameter("@Create_at", Fecha), new SqlParameter("@UserId", IdCliente));

                //var sale = db.Sales.Select(x => new { Id = x.Id, Total = x.Total }).ToList().Select(x => new Sale() { Id = x.Id, Total = x.Total }).ToList();
                if (SaleId == null)
                {
                    mensaje = "ERROR AL REGISTRAR LA VENTA";
                }
                else
                {                    
                        int SDId = 0;
                        //SALEDETAIL REGISTER
                        foreach (var data in ListadoDetalle)
                        {
                            SDId = random.Next(999999);
                            int idProducto = Convert.ToInt32(data.Id.ToString());
                            int cantidad = Convert.ToInt32(data.Quantity.ToString());
                            double descuento = Convert.ToDouble(data.Discount.ToString());
                            double subtotal = Convert.ToDouble(data.SubTotal.ToString());

                            db.Database.ExecuteSqlCommand(ResourceSQL.CreateBookingDetailsName + "@id, @subtotal, @quantity, @bookingid, @productid", new SqlParameter("@id", SDId), new SqlParameter("@subtotal", subtotal), new SqlParameter("@quantity", cantidad), new SqlParameter("Bookingid", SaleId), new SqlParameter("@productid", idProducto));
                        }

                        mensaje = "VENTA GUARDADA CON EXITO...";
                }

            }
            return Json(mensaje);
        }
        public void carga(int? id)
        {
            //cargarProductocmb();
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            List<Product> product = (from s in db.Products where s.Id == id select s).ToList();

            //if (product == null)
            //{
            //    return HttpNotFound();
            //}
            //cargarProductocmb();
            ViewBag.product = product;
        }
        
        [HttpGet]
        public ActionResult ObtainingProduct()
        {
            List<Product> products = db.Products.ToList();
            return View(products);
        }

        [HttpPost]//para buscar clientes
        public ActionResult ObtainingProduct(string txtName, string txtPrice, string txtQuantity, string txtId)
        {
            int txtid = Convert.ToInt32(txtId);
            int txtprice = Convert.ToInt32(txtPrice);
            int txtquantity = Convert.ToInt32(txtQuantity);            
            if (txtName == "")
            {
                txtName = "-1";
            }
            if (txtPrice == "")
            {
                txtPrice = "-1";
            }
            if (txtQuantity == "")
            {
                txtQuantity = "-1";
            }
            var objProduct = db.Products.ToList();
            objProduct[0].Id = txtid;
            objProduct[0].Name = txtName;
            objProduct[0].Price = txtprice;
            objProduct[0].Quantity = txtquantity;
            return View(objProduct);
        }
    }
}