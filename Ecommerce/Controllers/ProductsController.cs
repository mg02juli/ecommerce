using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class ProductsController : Controller
    {


        public ActionResult Index()
        {
            using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities())
            {
                List<Products> productList = DBEntities.Products.ToList();
                ViewBag.products = new List<Models.Products>(
                                productList.OrderByDescending(s => s.product_id)
                                .Select(x => new Models.Products
                                {
                                    product_id = x.product_id,
                                    product_name = x.product_name,
                                    product_price = x.product_price,
                                    Product_Image = x.Product_Image.Take(1).ToList(),
                                })
                                .Take(9)
                                .ToList());
            }
                ViewBag.category = "Latest Products";
                return PartialView("~/Views/Products/Latest.cshtml");
            }

        public ActionResult Featured()
        {
            using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities())
            {
                List<Products> productList = DBEntities.Products.ToList();
                ViewBag.products = new List<Models.Products>(
                                productList.Where(s => s.special_offer == "Sale")
                                .Select(x => new Models.Products
                                {
                                    product_id = x.product_id,
                                    product_name = x.product_name,
                                    product_price = x.product_price,
                                    special_offer = x.special_offer,
                                    Product_Image = x.Product_Image.Take(1).ToList(),
                                })
                                .Take(9)
                                .ToList());
            }
            ViewBag.category = "Latest Products";
            return PartialView("~/Views/Products/Featured.cshtml");
        }

        public ActionResult Products(int id)
        {
            using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities())
            {
                List<Products> productList = DBEntities.Products.ToList();
                ViewBag.products = new List<Models.Products>(
                                productList.Select(x => new Models.Products
                                {
                                    product_id = x.product_id,
                                    product_name = x.product_name,
                                    product_price = x.product_price,
                                    product_description = new string(x.product_description.TakeWhile(c => c != '.').ToArray()),
                                    Product_Image = x.Product_Image.Take(1).ToList(),
                                    product_type_code = x.product_type_code
                                })
                                .Where(s => s.product_type_code == id).ToList());
            }
            return View("Products");
        }

        public ActionResult Single(int id)
        {
            var product = new Products();
            using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities())
            {
                product = DBEntities.Products.Find(id);

                

            }
            return View(product);
        }

       


    }
}