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
    }
}