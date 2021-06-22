using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMVC.Controllers;

namespace WebAppMVC.Models
{
   
    public class CartController : Controller
    {
        private Sports_Zone_DbEntities db = new Sports_Zone_DbEntities();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        private int isExisting(string id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProId == id)
                {
            
                    return i;
                }    
            }
            return -1;
        }
        public ActionResult Delete(string id)
        {
            int index = isExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return View("cart");
        }
        public ActionResult OrderNow(string id)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(db.Products.Find(id), 1));
                Session["cart"] = cart;
               
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                {
                    cart.Add(new Item(db.Products.Find(id), 1));
                }
                else 
                {
                    cart[index].quantity++;
                }
                Session["cart"] = cart;
            }
                return View("cart"); 
         

        }
        public ActionResult Order()
        {
            return RedirectToAction("UserSignIn","Users");
        }
        
        public ActionResult CheckOut()
        {
           
            return View();
        }
    }
}