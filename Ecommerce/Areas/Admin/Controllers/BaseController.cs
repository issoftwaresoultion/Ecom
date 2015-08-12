using Ecommerce.DataAccess;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Admin/Base/

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
        public JsonResult IsUniqueEmail(string Email, string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return Json(AdminUserDal.IsUnique(Email, Convert.ToInt32(id)), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(AdminUserDal.IsUnique(Email, 0), JsonRequestBehavior.AllowGet);
            }
        }

    }
}
