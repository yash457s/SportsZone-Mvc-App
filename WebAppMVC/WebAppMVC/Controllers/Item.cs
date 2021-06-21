using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class Item
    {
        private Product product = new Product();
        public Product Product
           
        {
            get { return product; }
            set { product = value; }
        }
        public int quantity;
        private int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public Item()
        {

        }
        public Item(Product product, int quantity)
        {
            this.product = product;
            this.quantity = quantity;
        }
    }
}