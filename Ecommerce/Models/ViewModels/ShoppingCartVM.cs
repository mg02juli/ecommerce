using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public List<Carts> cart_items { get; set; }
        public decimal cart_total { get; set; }
    }
}