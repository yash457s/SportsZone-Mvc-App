using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private Sports_Zone_DbEntities db = new Sports_Zone_DbEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Search(string name)
        {
            var products = db.Products.ToList();
            if (name != null)
            {
                products = db.Products.Where(x => x.ProName == name).ToList();
            }
            return View(products);
        }
       
    }
}