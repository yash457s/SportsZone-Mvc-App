using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppMVC.Models
{
    public class SearchController : Controller
    {
        private Sports_Zone_DbEntities db = new Sports_Zone_DbEntities();
        // GET: Search
        public ActionResult Index()
        {
            return View(db.Search(""));
        }
        [HttpPost]
        public ActionResult Index(string text)
        {
            return View(db.Search(text));
        }
    }
}