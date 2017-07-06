using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using System.Data.Entity;

namespace Ecommerce.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        private WEBprojectDBEntities DBEntities =new WEBprojectDBEntities();
        public ActionResult Menu()
        {
            ViewBag.menuLevel = this.DBEntities.Ref_Product_Type.Where(menu => menu.product_parent_type_code == null).ToList();
            return PartialView("~/Views/Shared/Menu.cshtml");

        }

        public ActionResult Products() {

            List<Products> productList = DBEntities.Products.ToList();
            ViewBag.products = new List<Models.Products>(
                            productList.Select(x => new Models.Products
                            {
                                product_id = x.product_id,
                                product_name = x.product_name,
                                product_price = x.product_price,
                                Product_Image = x.Product_Image.Take(1).ToList(),
                            })
                            .Take(9)
                            .ToList());

            ViewBag.category = "Latest Products";
            return PartialView("~/Views/Shared/_Products.cshtml");
        }
    }
}