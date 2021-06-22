using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMVC.Filter;

namespace WebAppMVC.Models
{
    public class AdminsController : Controller
    {
        private Sports_Zone_DbEntities db = new Sports_Zone_DbEntities();

        // GET: Admins
        [OurAuthFilter]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("SignIn", "Admins");
        }
        [HttpGet]
        public ActionResult SignIn()
        {
            return View(new Models.Admin());
        }
        [HttpPost]
        public ActionResult SignIn(Models.Admin obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            else
            {
                using (var context = new Sports_Zone_DbEntities())
                {
                    Models.Admin admin = context.Admins.Where(u => u.AdmEmail == obj.AdmEmail && u.AdmPassword == obj.AdmPassword).FirstOrDefault();

                    if (admin != null)
                    {
                        Session["AdminEmail"] = admin.AdmEmail.ToString();
                        return RedirectToAction("Index", "Admins");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid user Email or Password");
                        return View(obj);
                    }
                }

            }
        }
    }
}