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


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View("Contact");
        }

    }
}

