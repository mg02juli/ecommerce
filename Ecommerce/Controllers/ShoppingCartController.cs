using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using Ecommerce.Models.ViewModels;

namespace Ecommerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        WEBprojectDBEntities DBEntities= new WEBprojectDBEntities();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartVM
            {
                cart_items = cart.GetCartItems(),
                cart_total = cart.GetTotal()
            };


            return View(viewModel);
        }


        // GET: AddToCart 
        public ActionResult AddToCart(int id)
        {
            // Retrieve the product from the database
            var addProduct = DBEntities.Products
                .Single(s => s.product_id == id);

            // Add product to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addProduct);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        // AJAX: RemoveFromCart
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Get cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the product to display confirmation
            string productName = DBEntities.Carts
                .Single(item => item.cart_id == id).Products.product_name;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveVM
            {
                message = Server.HtmlEncode(productName) +
                    " has been removed from your shopping cart.",
                cart_total = cart.GetTotal(),
                cart_count = cart.GetCount(),
                item_count = itemCount,
                dalete_id = id
            };
            return Json(results);
        }


        // GET: CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }


    }
}