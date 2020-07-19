using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private class Name
        {
            public string name { get; set; }
        }

        private class Images
        {
            public int id { get; set; }
            public string name { get; set; }
            public decimal price { get; set; }
            public byte[] image { get; set; }
        }
        public ActionResult Index()
        {
            var _estaEnRol = false;
            using (ApplicationDbContext dbs = new ApplicationDbContext())
            {
                var idUsuario = User.Identity.GetUserId();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbs));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbs));
                if (idUsuario != null)
                {
                    var _users = dbs.Database.SqlQuery<Name>(ResourceSQL.ListRoleName).ToList();
                    for (int i = 0; i < _users.LongCount(); i++)
                    {
                        _estaEnRol = userManager.IsInRole(idUsuario, _users[i].name);
                        if (_estaEnRol == true)
                        {
                            return View();
                        }
                    }
                    if (_estaEnRol == false)
                    {
                        var _result = userManager.AddToRole(idUsuario, "Cliente");
                    }
                }
            }

            List<Product> _product = db.Database.SqlQuery<Product>(ResourceSQL.RandomImageName).ToList();
            ViewBag.product = _product;
            //var _product = (from d in db.Products select d).ToList();
            return View(_product);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{
            //    var idUsuario = User.Identity.GetUserId();
            //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            //    //CREAATE ROLE
            //    var _result = roleManager.Create(new IdentityRole("Gerente"));
            //    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //    //ASIGNAR USUARIOS AL ROL
            //    _result = userManager.AddToRole(idUsuario, "Gerente");

            //    //USER IN THE ROLE
            //    var _estaEnRol = userManager.IsInRole(idUsuario, "Gerente");
            //    var _estaEnRol2 = userManager.IsInRole(idUsuario, "Cliente");

            //    // ROLE OF USER
            //    var _role = userManager.GetRoles(idUsuario);

                ////REMOVE TO USER OF ROLE
                //_result = userManager.RemoveFromRole(idUsuario, "Gerente");

                //DELETE ROLE
                //    var _deleteRole = roleManager.FindByName("Gerente");
                //    roleManager.Delete(_deleteRole);
            //}
            //return RedirectToAction("Index","Products");
            return RedirectToAction("Index", "Products");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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
    }
}