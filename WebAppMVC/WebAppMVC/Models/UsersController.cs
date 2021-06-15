using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebAppMVC.Models
{
    public class UsersController : Controller
    {
        private Sports_Zone_DbEntities db = new Sports_Zone_DbEntities();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Customer_info).Include(u => u.UserLogin);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.Customer_info, "CusId", "First_Name");
            ViewBag.userId = new SelectList(db.UserLogins, "UserId", "password");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userId,password,first_name,last_name,email,UserAddress,UserMobile")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.Customer_info, "CusId", "First_Name", user.userId);
            ViewBag.userId = new SelectList(db.UserLogins, "UserId", "password", user.userId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.Customer_info, "CusId", "First_Name", user.userId);
            ViewBag.userId = new SelectList(db.UserLogins, "UserId", "password", user.userId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userId,password,first_name,last_name,email,UserAddress,UserMobile")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.Customer_info, "CusId", "First_Name", user.userId);
            ViewBag.userId = new SelectList(db.UserLogins, "UserId", "password", user.userId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UserSignIn()
        {
            return View(new Models.User());
        }
        [HttpPost]
        public ActionResult UserSignIn(Models.User obj)
        {
            if (!ModelState.IsValid)
                return View(obj);
            else
            {
                using (var context = new Sports_Zone_DbEntities())
                {
                    Models.User user = context.Users.Where(u => u.email == obj.email && u.password == obj.password).FirstOrDefault();
                    if (user != null)
                    {
                        Session["UserEmai"] = user.email;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid User Email or password");
                        return View(obj);
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
