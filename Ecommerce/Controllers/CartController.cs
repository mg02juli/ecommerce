using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Cart()
        {
            #region
            //var cart = Session["cart"] as List<Cart> ?? new List<Cart>();
            //if (cart.Count == 0 || Session["cart"] == null)
            //{
            //    ViewBag.Message = "Cart is empty";
            //}
            //else
            //{
            //    ViewBag.grandTotal = cart.Sum(s=>s.total);

            //}
            #endregion

            return View();
        }



        public ActionResult CartPartial()
        {
            #region
            //Cart cart = new Cart();
            //int quantity = 0;
            //double price = 0.0;

            //if (Session["cart"] != null)
            //{
            //    var list = (List<Cart>)Session["cart"];

            //    //foreach (var item in list)
            //    //{
            //    //    quantity += item.quantity;
            //    //    price += item.quantity * item.product.product_price;
            //    //}
            //    cart.quantity = list.Sum(s => s.quantity);
            //    //price= list.Aggregate
            //    // price = list.Aggregate(1.0, (x, y)=> Convert.ToDouble(x) * Convert.ToDouble(y));
            //    //  item.quantity * item.product.product_price;
            //}
            //else
            //{
            //    cart.quantity = 0;
            //    //cart.total = 0.0;
            //}
            #endregion
            return PartialView();
        }


        public void AddToCart(int id)
        {
            #region
            //using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities())
            //{

            //    var cart = Session["cart"] as List<Cart> ?? new List<Cart>();

            //    var pr = DBEntities.Products.Find(id);
            //    cart.Add(new Cart()
            //    {
            //        product = pr,
            //        quantity = 1
            //    });
            //    Session["cart"] = cart;
            //}
            ////return RedirectToAction("Cart", "Cart");
            ////Redirect(ReturnUrl)

            #endregion

        }


    }
}