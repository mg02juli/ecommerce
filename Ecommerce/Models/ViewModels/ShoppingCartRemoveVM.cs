using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.ViewModels
{
    public class ShoppingCartRemoveVM
    {
        public string message { get; set; }
        public decimal cart_total { get; set; }
        public int cart_count { get; set; }
        public int item_count { get; set; }
        public int dalete_id { get; set; }
    }
}