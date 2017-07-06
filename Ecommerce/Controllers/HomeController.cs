using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        // private ICategoryRep iCategoryRep= 
        private WEBprojectDBEntities DBEntities = new WEBprojectDBEntities();


        public ActionResult Index(int? id)
        {
            ViewBag.products_category = id;
            return View();
        }
        public ActionResult Contact()
        {
            return View("Contact");
        }
        public ActionResult Single()
        {
            return View("Single");
        }



        public ActionResult Products(int id)
        {

            List<Products> productList = DBEntities.Products.ToList();
            ViewBag.products = new List<Models.Products>(
                            productList.Select(x => new Models.Products
                            {
                                product_id = x.product_id,
                                product_name = x.product_name,
                                product_price = x.product_price,
                                product_description =  new string(x.product_description.TakeWhile(c => c != '.').ToArray()),
            Product_Image = x.Product_Image.Take(1).ToList(),
                                product_type_code = x.product_type_code
                            })
                            .Where(s => s.product_type_code == id).ToList());

            return View("Products");
        }
    }
}

