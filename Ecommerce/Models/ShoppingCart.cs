using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Models
{
    public partial class ShoppingCart
    {
        string shopping_cart_identifier { get; set; }
        public const string cart_session_key = "CartID";

        public static ShoppingCart GetCart(HttpContextBase context) {
            var cart = new ShoppingCart();
            cart.shopping_cart_identifier = cart.GetCartId(context);
            return cart;
        }

        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }



        public void AddToCart(Products product)
        {
            using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities()) {
                // Get the matching cart and album instances
                var cartItem = DBEntities.Carts.SingleOrDefault(
                    c => c.identifier == shopping_cart_identifier
                    && c.product_id == product.product_id);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Carts
                {
                    product_id = product.product_id,
                    identifier = shopping_cart_identifier,
                    quantity = 1,
                    date_created = DateTime.Now
                };
                DBEntities.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.quantity++;
            }
            // Save changes
            DBEntities.SaveChanges();
            }
        }


        public int RemoveFromCart(int id) {
            int itemCount = 0;
            using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities())
            {
                var cartItem = DBEntities.Carts.Single(
                    cart => cart.identifier == shopping_cart_identifier
                    && cart.cart_id == id
                    );
                

                if (cartItem != null)
                {
                    if (cartItem.quantity > 1)
                    {
                        cartItem.quantity--;
                        itemCount = cartItem.quantity;
                    }
                    else
                    {
                        DBEntities.Carts.Remove(cartItem);
                    }
                    DBEntities.SaveChanges();
                }// DBEntities.Carts.Remove(cartItems)
            }
            return itemCount;
        }


        public void EmptyCart() {
            using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities()) {
                var cartItems = DBEntities.Carts.Where(
                    s => s.identifier.Equals(shopping_cart_identifier)).ToList();

                DBEntities.Carts.RemoveRange(cartItems);
                DBEntities.SaveChanges();
            }

        }

        public List<Carts> GetCartItems()
        {
            using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities())
            {
                return DBEntities.Carts.Where(
                cart => cart.identifier == shopping_cart_identifier).ToList();
            }

        }

        public int GetCount() {

            int count = 0;
            // Get the count of each item in the cart and sum them up
            using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities()) {
                count = DBEntities.Carts
                    .Where(
                        s => s.identifier.Equals(shopping_cart_identifier))
                    .Select(
                    s => s.quantity).Sum();

            }
            // Return 0 if all entries are null
            return count;
        }

        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal total = 0m;
            using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities()) {
                total = DBEntities.Carts
                    .Where(
                    s => s.identifier.Equals(shopping_cart_identifier))
                    .Select(s=>s.quantity * s.Products.product_price).Sum();
            }
            return total;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0m;
            using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities())
            {
                var cartItems = GetCartItems();
                // Iterate over the items in the cart, 
                // adding the order details for each
                foreach (var item in cartItems)
                {
                    var order_Items = new Order_Items
                    {
                        product_id = item.product_id,
                        order_id = order.order_id,
                        order_item_price = item.Products.product_price,
                        order_item_quantity = item.quantity
                    };
                    // Set the order total of the shopping cart
                    orderTotal += (item.quantity * item.Products.product_price);

                    DBEntities.Order_Items.Add(order_Items);

                }
                // Set the order's total to the orderTotal count
                order.order_total = orderTotal;

                // Save the order
                DBEntities.SaveChanges();
                // Empty the shopping cart
                EmptyCart();
                // Return the OrderId as the confirmation number
                return order.order_id;
            }
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[cart_session_key] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[cart_session_key] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[cart_session_key] = tempCartId.ToString();
                }
            }
            return context.Session[cart_session_key].ToString();
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities())
            {
                var shoppingCart = DBEntities.Carts.Where(
                    c => c.identifier == shopping_cart_identifier);

                foreach (Carts item in shoppingCart)
                {
                    item.identifier = userName;
                }
                DBEntities.SaveChanges();
            }
        }





    }
}