using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.DataAccess;
using System.Web.Security;


namespace Ecommerce.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Admin/Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Validate(LoginModel obj)
        {

            obj.isadmin = true;
            var check = UserDal.ValidateUser(obj);
            if (check!=null)
            {
                FormsAuthentication.SetAuthCookie("admin", false);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                1,                                     // ticket version
                                 check.Email,                              // authenticated username
                                 DateTime.Now,                          // issueDate
                                 DateTime.Now.AddDays(2),           // expiryDate
                                 false,                          // true to persist across browser sessions
                                 check.Name + "-" + check.id,                              // can be used to store additional user data
                                 FormsAuthentication.FormsCookiePath);
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Category");
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid Username and password";
                return RedirectToAction("Index", "home");
            }
        }

        public ActionResult Logout()
        {
            Logout();
            return RedirectToAction("Index", "home");
        }
        
    }
}
