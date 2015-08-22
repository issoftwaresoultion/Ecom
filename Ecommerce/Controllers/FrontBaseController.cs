using Ecommerce.DataAccess;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ecommerce.Controllers
{
    public class FrontBaseController : Controller
    {
        protected LoginModel GetLoginUserdata()
        {
            LoginModel obj = new LoginModel();
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                string userData = ticket.UserData;
                obj.Name = userData.Split('-')[0];
                obj.id = Convert.ToInt32(userData.Split('-')[1]);
            }
            return obj;
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Home", "Index");
        }

        public ActionResult GetSignup()
        {
            if (Request.IsAuthenticated)
            {
                var obj = GetLoginUserdata();
                return PartialView("_LoginPartial", obj);
            }
            else
            {
                return PartialView("_LoginPartial");
            }
        }

        public ActionResult GetFreeDeleveryCharges()
        {
            var Model = DeliveryDal.GetDefault();
            string charge = "";
            if (Session["currency"].ToString() == "Dollar")
            {
                charge = "$" + Model.freeDeliveryAmountDoller;
            }
            if (Session["currency"].ToString() == "Euro")
            {
                charge = "&euro" + Model.deliveryEuro;
            }
            if (Session["currency"].ToString() == "Pound")
            {
                charge = "&pound;" + Model.freeDeliveryAmountPound;
            }
            if (Session["currency"].ToString() == "Naira")
            {
                charge = "&#8358;" + Model.freeDeliveryAmountNigria;

            }
            if (Session["currency"].ToString() == "Kwanza")
            {
                charge = "Kz" + Model.freeDeliveryAmountAngola;
            }
            if (Session["currency"].ToString() == "Cedi")
            {
                charge = "GH₵" + Model.freeDeliveryAmountGhana;
            }
            return Content(charge);
        }

        public JsonResult IsUniqueEmail(string Email, string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return Json(UserDal.IsUnique(Email, Convert.ToInt32(id)), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(UserDal.IsUnique(Email, 0), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SendEmailForgotPassword(LoginModel obj)
        {
            var OutputMessage = "";
            var user = UserDal.GetByemail(obj.EmailForgotPassword);
            if (user.Name != null)
            {
                MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["Email"], "Ecommerce");
                MailAddress toAddress = new MailAddress(obj.EmailForgotPassword, "Ishu");
                string fromPassword = ConfigurationManager.AppSettings["Password"];
                string subject = "Your New Password";
                string body = "your new password is " + user.Email;
                var smtp = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["Host"],
                    Port = 8889,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);

                }
                OutputMessage = "Password is send at your email address.";
            }
            else
            {
                OutputMessage = "This email address is not registerd.";
            }
            return Json(OutputMessage);
        }


        public JsonResult RegisterUser(LoginModel obj)
        {
            bool check=false;
            check = UserDal.RegisterUser(obj);
            return Json("You have successfully registered");
        }

        public ContentResult ReturnPolicy()
        {
            string policy = StaticPagesDal.GetByHeading("Return policy").Content;
            return (Content(policy));
        }
    }
}
