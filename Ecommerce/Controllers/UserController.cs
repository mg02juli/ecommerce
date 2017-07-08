using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
namespace Ecommerce.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
                     
            return View("Login");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View("Register");
        }


        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            //WEBprojectDBEntities DBEntities = new WEBprojectDBEntities();
            if (ModelState.IsValid) {
                using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities()) {
                    DBEntities.Customer.Add(customer);
                    DBEntities.SaveChanges();
                    Response.Write("<script> alert('Success')</script>");
                }
            }

            //RedirectToAction("Register", "User")
            return View();
        }


        public ActionResult Checkout()
        {
            return View("Checkout");
        }
    }
}