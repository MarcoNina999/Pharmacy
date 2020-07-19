using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy.Controllers
{
    [Authorize]
    public class ManageUsersController : Controller
    {
        //User Manager context
        ApplicationDbContext context;
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
       
        // Default Constructor
        public ManageUsersController()
        {
            context = new ApplicationDbContext();
        }

        // GET: ManageUser        
        public ActionResult Index()
        {
            return RedirectToAction("UsersRoles");
        }

        // GET: ManageUser/CreateRol
        public ActionResult CreateRole()
        {
            var roles = context.Roles.OrderBy(r => r.Name).ToList();
            ViewBag.ResultMessage = "";
            return View(roles);
        }

        // POST: ManageUser/CreateRole
        [HttpPost]
        public async Task<ActionResult> CreateRole(FormCollection collection)
        {
            try
            {
                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                await context.SaveChangesAsync();
                ViewBag.ResultMessage = collection["RoleName"].ToString();

                var roles = await context.Roles.OrderBy(r => r.Name).ToListAsync();

                return View("CreateRole", roles);
            }
            catch
            {
                return View();
            }
        }

        // POST: ManageUser/DeleteRole
        public ActionResult DeleteRole(string RoleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: ManageUser/UsersRoles
        public ActionResult UsersRoles()
        {
            var vm = UserRolesViewModel();

            return View(vm);
        }

        public UserRolesViewData UserRolesViewModel()
        {
            var roles = context.Roles.Include(r => r.Users).OrderBy(r => r.Name).AsQueryable();
            var users = context.Users.OrderBy(u => u.UserName).AsQueryable();


            var userRoles = new List<RolesByUser>();


            foreach (var user in users)
            {

                var rolesUser = UserManager.GetRoles(user.Id).ToList();

                var roleUser = new List<RoleUserExist>();

                foreach (var role in rolesUser)
                {
                    roleUser.Add(new RoleUserExist
                    {
                        RoleName = role,
                        HasRole = true
                    });
                }

                userRoles.Add(new RolesByUser
                {
                    UserName = user.UserName,
                    RolesUser = roleUser
                });
            }

            var vm = new UserRolesViewData();
            vm.RolesByUser = userRoles;
            return vm;
        }



        /*****************************************************************************/
        //
        // GET: /ManageUsersProsmart/AssignUserRoles
        public ActionResult AssignUserRoles(string userName)
        {
            var vm = GetAssignUserRoles(userName);
            return View(vm);
        }

        private RolesByUser GetAssignUserRoles(string userName)
        {
            var roles = context.Roles.Include(r => r.Users).OrderBy(r => r.Name).AsQueryable();
            var user = context.Users.FirstOrDefault(u => u.UserName == userName);

            var roleUser = new List<RoleUserExist>();

            foreach (var role in roles)
            {
                roleUser.Add(new RoleUserExist
                {
                    RoleName = role.Name,
                    HasRole = UserManager.IsInRole(user.Id, role.Name)
                });
            }

            var vm = new RolesByUser();
            vm.UserName = userName;
            vm.RolesUser = roleUser;
            return vm;
        }
        //
        // POST: /ManageUsersProsmart/AssignUserRoles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignUserRoles(string userName, string[] userRoles)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == userName); //get user

            var newRoles = userRoles; //new roles from view
            var currentRoles = UserManager.GetRoles(user.Id).ToArray();//current user's roles

            UserManager.RemoveFromRoles(user.Id, currentRoles);//Clear ALL current roles for user            
            await UserManager.AddToRolesAsync(user.Id, newRoles);

            return RedirectToAction("UsersRoles");
        }
    }



    //ViewModels for views and such...
    //------------------------------------------------------------
    public class AssignRolesUsersViewData
    {
        public IEnumerable<string> users { get; set; }
        public IEnumerable<string> roles { get; set; }
    }

    public class UserRolesViewData
    {
        public IEnumerable<RolesByUser> RolesByUser { get; set; }
    }

    public class RolesByUser
    {
        public RolesByUser()
        {
            this.RolesUser = new List<RoleUserExist>();
        }
        public string UserName { get; set; }
        public List<RoleUserExist> RolesUser { get; set; }
    }

    public class RoleUserExist
    {
        public RoleUserExist()
        {
            this.HasRole = true;
        }
        public string RoleName { get; set; }
        public bool HasRole { get; set; }
    }

}