using ArtStationWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ArtStattion.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(ArtStationWebApi.Models.Membership mem)
        {
            using (var db = new DBModel())
            {
                bool isValid = db.Users.Any(m=>m.UserName==mem.UserName && m.Password==mem.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(mem.UserName,false);
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("","Invalid Username or password");
                }
            }
            
            return View();
        }



        // GET: Account
        public ActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Signup(User mem)
        {
            using (var db = new DBModel())
            {
                db.Users.Add(mem);
                var userrole = new UserRole()
                {
                    UserId = mem.Id,
                    Role = "User"
                };
                db.UserRoles.Add(userrole);
                db.SaveChanges();
                
            }
            return RedirectToAction("Login");
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}