using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Pharmacy.Controllers
{
    public class SaleController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();        

        public SaleController()
        {
        }

        [HttpGet]
        public ActionResult ObtainingClient()
        {
            List<ApplicationUser> users = db.Database.SqlQuery<ApplicationUser>(ResourceSQL.ListUsersName).ToList();
            return View(users);
        }

        [HttpPost]//para buscar clientes
        public ActionResult ObtainingClient(string txtFirstName, string txtLastName, string txtCi, string txtId)
        {
            int txtci = Convert.ToInt32(txtCi);
            if (txtFirstName == "")
            {
                txtFirstName = "-1";
            }
            if (txtLastName == "")
            {
                txtLastName = "-1";
            }
            if (txtCi == "")
            {
                txtCi = "-1";
            }
            var objUsers = db.Users.ToList();
            objUsers[0].First_Name = txtFirstName;
            objUsers[0].Last_Name = txtLastName;
            objUsers[0].Id = txtId;
            objUsers[0].Ci = txtci;
            return View(objUsers);
        }

        [HttpPost]
        public ActionResult Select(int id)
        {
            //var objProduct = db.Products.Include(x => x..Where(y => y.Id = Id)).ToList();
            var objProduct = (from s in db.Products where s.Id == id select s);

            return Json(objProduct, JsonRequestBehavior.AllowGet);

        }
        //public ActionResult PruebaJson()
        //{  // escribir la url directa  para ver el formato                  
        //    var product = from s in db.Products select s;
        //    return Json(product, JsonRequestBehavior.AllowGet);

        //}

        public void cargarProductocmb()
        {
            var product = from s in db.Products select s;
            SelectList list = new SelectList(product, "Id", "Name");
            ViewBag.ListProduct = list;
        }
        public void cargarModoPagocmb()
        {
            //List<ModoPago> data = objModoPagoNeg.findAll();
            //SelectList lista = new SelectList(data, "numPago", "nombre");
            //ViewBag.ListaModoPago = lista;
        }

        public ActionResult NewSale()
        {
            cargarModoPagocmb();
            cargarProductocmb();
            return View();
        }
        [HttpPost]
        public ActionResult GuardarVenta(string Fecha, string modoPago, string IdCliente, string Total, List<SaleDetails> ListadoDetalle)
        {
            Random random = new Random();
            string mensaje = "";
            double iva = 49;
            string idVendedor = "321";
            int codigoPago = 0;
            string codigoCliente = "";
            double total = 0;

            if (Fecha == "" /*|| modoPago == ""*/ || IdCliente == "" || Total == "")
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
                db.Database.ExecuteSqlCommand(ResourceSQL.CreateSaleName + "@id, @total, @Create_at, @iva, @userId", new SqlParameter("@id", SaleId), new SqlParameter("@total", total), new SqlParameter("@Create_at", Fecha), new SqlParameter("@iva", iva), new SqlParameter("@userId", IdCliente));
                
                //var sale = db.Sales.Select(x => new { Id = x.Id, Total = x.Total }).ToList().Select(x => new Sale() { Id = x.Id, Total = x.Total }).ToList();
                if (SaleId == null)
                {
                    mensaje = "ERROR AL REGISTRAR LA VENTA";
                }
                else
                {
                    int Numbill = 0;
                    //BILL REGISTER
                    foreach (var data in ListadoDetalle)
                    {
                        Numbill = random.Next(999999);
                        double descuento = Convert.ToDouble(data.Discount.ToString());
                        db.Database.ExecuteSqlCommand(ResourceSQL.CreateBillName + "@numbill, @create_at, @total, @discount", new SqlParameter("@numbill", Numbill), new SqlParameter("@create_at", Fecha), new SqlParameter("@total", total), new SqlParameter("@discount", descuento));
                    }
                    var codbill = db.Bills.Select(x => new { Id = x.numBill }).ToList().Select(x => new Bill() { numBill = x.Id }).ToList();
                    if (Numbill == null)
                    {
                        mensaje = "ERROR AL REGISTRAR LA FACTURA";
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

                            db.Database.ExecuteSqlCommand(ResourceSQL.CreateSaleDetailsName + "@id, @numbill, @subtotal, @discount, @quantity, @saleid, @productid", new SqlParameter("@id", SDId), new SqlParameter("@numbill", Numbill), new SqlParameter("@subtotal", subtotal), new SqlParameter("@discount", descuento), new SqlParameter("@quantity", cantidad), new SqlParameter("saleid", SaleId), new SqlParameter("@productid", idProducto));
                        }
                        HttpCookie coockorder = new HttpCookie("orden");
                        coockorder["idorden"] = SDId.ToString();

                        Response.Cookies.Add(coockorder);
                        
                        mensaje = "VENTA GUARDADA CON EXITO...";
                    }
                }

            }
            return Json(mensaje);
        }

        //public ActionResult reporteActual()
        //{
        //    if (Session["idVenta"].ToString() != null)
        //    {
        //        string idVenta = Session["idVenta"].ToString();
        //        return Redirect("~/Reportes/frmReporteFactura.aspx?IdVenta=" + idVenta);
        //    }
        //    else
        //    {
        //        return View("GuardarVenta");
        //    }

        //}

        //public ActionResult ReporteVentas()
        //{
        //    List<Venta> lista = objVentaNeg.findAll();
        //    return View(lista);
        //}

        //public ActionResult DetallesVenta(long id)
        //{
        //    DetalleVenta objDetalleVenta = new DetalleVenta();
        //    objDetalleVenta.IdVenta = id;
        //    List<DetalleVenta> lista = objDetalleVentaNeg.detallesPorIdVenta(objDetalleVenta);
        //    return View(lista);
        //}

        //public ActionResult Bill()
        //{
        //    //List<SaleDetails> lista = objventaneg.findall();
        //    var sale = from s in db.SaleDetails select s;
        //    return View(sale);
        //}
    }
}
