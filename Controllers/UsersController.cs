
using Travel_App.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Travel_App.Controllers
{
    // poate fi accesat doar de catre Admin
    [Authorize(Roles = "Admin")]


    public class UsersController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Users
        public ActionResult Index()
        {
            ViewBag.UsersList = _context.Users
                .OrderBy(u => u.UserName)
               .ToList();
            return View();
            return View();
        }

        public ActionResult Details(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return HttpNotFound("Missing the id parameter!");
            }

            ApplicationUser user = _context.Users
            .Include("Roles")
            .FirstOrDefault(u => u.Id.Equals(id));

            if (user != null)
            {
                ViewBag.UserRole = _context.Roles
                .Find(user.Roles.First().RoleId).Name;
                return View(user);
            }
            return HttpNotFound("Cloudn't find the user with given id!");
        }

        public ActionResult Edit(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return HttpNotFound("Missing the id parameter!");
            }

            UserViewModel uvm = new UserViewModel();
            uvm.User = _context.Users.Find(id);

            IdentityRole userRole = _context.Roles.Find(uvm.User.Roles.First().RoleId);
            uvm.RoleName = userRole.Name;
            return View(uvm);
        }

        [HttpPut]
        public ActionResult Edit(string id, UserViewModel uvm)
        {
            ApplicationUser user = _context.Users.Find(id);
            try
            {
                if (TryUpdateModel(user))
                {
                    var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

                    foreach (var r in _context.Roles.ToList())
                    {
                        um.RemoveFromRole(user.Id, r.Name);
                    }
                    um.AddToRole(user.Id, uvm.RoleName);

                    user.UserName = uvm.User.Email;
                    user.Email = uvm.User.Email;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View(uvm);
            }
        }
    }
}