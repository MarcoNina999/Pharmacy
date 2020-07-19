using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;
using Pharmacy.Models;

namespace Pharmacy.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ViewResult Index( string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.dateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var product = from s in db.Products select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Name_desc":
                    product = product.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    product = product.OrderBy(s => s.Create_at);
                    break;
                case "date_desc":
                    product = product.OrderByDescending(s => s.Create_at);
                    break;
                default:
                    product = product.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            Product product = new Product();
            product.Create_at = DateTime.Today;
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Quantity,Price,Symptoms,Image,Create_at,Is_active")] Product product)
        {
            HttpPostedFileBase FileBase = Request.Files[0];
            
            WebImage image = new WebImage(FileBase.InputStream);

            product.Image = image.GetBytes();
            
            product.Create_at = DateTime.Today;

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Quantity,Price,Symptoms,Image,Create_at,Is_active")] Product product)
        {
            //byte[] previousimage = null;
            Product _product = new Product();

            HttpPostedFileBase FileBase = Request.Files[0];
            if(FileBase.ContentLength == 0)
            {
                _product = db.Products.Find(product.Id);
                product.Image = _product.Image;
            }
            else
            {
                WebImage image = new WebImage(FileBase.InputStream);
                product.Image = image.GetBytes();
            }
            if (ModelState.IsValid)
            {
                db.Entry(_product).State = EntityState.Detached;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult getImage(int id)
        {
            Product product = db.Products.Find(id);
            byte[] byteImage = product.Image;
            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);
            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");
        }
        //public async Task<List<object[]>> filtrarAjax(string Name)
        //{
        //    int coun = 0, cant;
        //    string datafilter = "";
        //    List<object[]> product = new List<object[]>();
        //    var _product = await _context.Product.ToListAsync();
        //    IEnumerable<Product> query;
        //    if(Name == "null")
        //    {
        //        query = from u in _product select u;
        //    }
        //    else
        //    {
        //        query = from u in _product where u.Name.StartWith(Name) select u;
        //    }
        //    cant = query.Count();
        //}
    }
}
