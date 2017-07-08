using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using System.Net.Mail;
using System.Net;
using System.Web.Security;

namespace Ecommerce.Controllers
{
    public class UserController : Controller
    {

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "is_email_verified, activation_code")]Customer customer)
        {
            string message = "";
            //Validate
            if (ModelState.IsValid)
            {
                //Check email
                #region // Email already exist
                var exist = IsEmailExist(customer.emailID);
                if (exist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(customer);
                }
                #endregion

                //Generate activation code
                #region // Generate activation code
                customer.activation_code = Guid.NewGuid();
                #endregion

                //Hash
                #region //Hash password
                customer.password = Crypto.Hash(customer.password);
                customer.cnf_password = Crypto.Hash(customer.cnf_password);
                #endregion
                customer.is_email_verified = false;

                //Save
                #region
                using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities())
                {
                    DBEntities.Customer.Add(customer);
                    DBEntities.SaveChanges();

                    //Send email to user
                    SendVerificationLinkEmail(customer.emailID, customer.activation_code.ToString());
                    message = "Registration succesfully done. Account actvation link" +
                        "has been sent to your email:" + customer.emailID;
                }
                #endregion

            }
            else
            {
                message = "Invalid Request";
            }



            ViewBag.Message = message;
            return View(customer);
        }

        //Verify Account
        [HttpGet]
        public ActionResult VerifyAccount(string id) // MAGIC!! works only when called "id". WHY?
        {
            bool Status = false;
            using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities()) {
                DBEntities.Configuration.ValidateOnSaveEnabled = false; // To resolve cnf_password does not match issue on save changes
                var v = DBEntities.Customer.Where(s=>s.activation_code == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.is_email_verified = true;
                    DBEntities.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }

            }
            ViewBag.Status = Status;
                return View();
        }

        //Login Get
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        //Login Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin userLogin, string ReturnUrl)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities())
                {
                    var v = DBEntities.Customer.Where(s=>s.emailID == userLogin.emailID).FirstOrDefault();
                    if (v != null)
                    {
                        if (string.Compare(Crypto.Hash(userLogin.password), v.password) == 0)
                        {
                            int timeout = userLogin.remember_me ? 52560 : 1; //52560 minutes equals 1 year
                            var ticket = new FormsAuthenticationTicket(userLogin.emailID, userLogin.remember_me, timeout);
                            string encrypt = FormsAuthentication.Encrypt(ticket);
                            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypt);
                            cookie.Expires = DateTime.Now.AddMinutes(timeout);
                            cookie.HttpOnly = true;
                            Response.Cookies.Add(cookie);

                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            message= "Invalid credential provided"; ;
                        }
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
            }
            ViewBag.Message = message;
            return View();
        }

        // Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        [NonAction]
        public bool IsEmailExist(string EmailID)
        {
            using (WEBprojectDBEntities DBEntities = new WEBprojectDBEntities())
            {
                var val = DBEntities.Customer.Where(s => s.emailID == EmailID).FirstOrDefault();
                return val != null;
            }
        }

        [NonAction]
        public void SendVerificationLinkEmail(string EmailID, string ActivationCode){
            var verifyUrl = "/User/VerifyAccount/" + ActivationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);


            var fromEmail = new MailAddress("myemaildummydummy@gmail.com");
            var toEmail = new MailAddress(EmailID);
            string fromEmailPassword = "LAKAPIENA";
            string subject = "Your account is successfully created!";
            string body = "<br/><br/> Account was successfully created! \n please dont buy anything" +
                " as this website is created for educational purposes. <br/> Please verify your account" +
                " <br/><br/><a href='" + link + "'> " + link + "</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
    }
}




