using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View("Login");
        }
        public ActionResult Register()
        {
            return View("Register");
        }
        public ActionResult Checkout()
        {
            return View("Checkout");
        }
    }
}