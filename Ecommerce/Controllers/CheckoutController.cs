using Ecommerce.DataAccess;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ecommerce.Controllers
{
    public class CheckoutController : FrontBaseController
    {
        //
        // GET: /Checkout/

        public ActionResult Index(string id)
        {
            var user = new SignupModel();
            ViewBag.DeliveryDays = DeliveryDal.GetDefault();
            ViewBag.RemoveDelete = "yes";
            ViewBag.id = 0;
            ViewBag.Faq = StaticPagesDal.GetBygroupId("Faq");
            if (TempData != null)
            {
                if (TempData["Invalidlogin"] == "Invalid Username and password")
                {
                    return View();
                }
            }
            if (Request.IsAuthenticated)
            {
                if (GetLoginUserdata().id > 0)
                {
                    ViewBag.id = GetLoginUserdata().id;
                    id = Convert.ToString(ViewBag.id);
                }
            }

            ViewBag.User = new Ecommerce.Model.SignupModel();
            if (id != null)
            {
                user = UserDal.GetById(Convert.ToInt32(id));
                ViewBag.User = user;
                ViewBag.id = user.id;
            }

            if (String.IsNullOrEmpty(Convert.ToString(Session["OrderId"])))
            {
                Session["OrderId"] = CartDal.SaveOrUpdateCartAsOrder((CartModel)Session["Cart"], Convert.ToString(Session["currency"]), user.id, 0);
            }
            else
            {
                Session["OrderId"] = CartDal.SaveOrUpdateCartAsOrder((CartModel)Session["Cart"], Convert.ToString(Session["currency"]), user.id, Convert.ToInt32(Session["OrderId"]));
            }
             var OrderId = new HttpCookie("OrderId")
                {
                    Value = Session["OrderId"].ToString()
                };
            Response.SetCookie(OrderId);
            return View();
        }

        [HttpPost]
        public ActionResult Save(SignupModel obj)
        {
            int userId = 0;
            var check = true;
            TempData["Message"] = "";
            obj.Password = Guid.NewGuid().ToString().Substring(0, 6);
            obj.Isadmin = false;
            if (obj.id > 0)
            {
                check = UserDal.Update(obj);
                
            }
            else
            {

                check = UserDal.Create(obj, out userId);
            }

            check = OrderDal.UpdateUserAddressInOrder(new OrderHeader
            {
               Userid=userId,
                Address1 = obj.Address1,
                Address2 = obj.Address2,
                City = obj.City,
                ContactNumber = obj.ContactNumber,
                Country = obj.Country,
                DAddress1 = obj.DAddress1,
                DAddress2 = obj.DAddress2,
                DCity = obj.DCity,
                DCountry = obj.DCountry,
                DPostCode = obj.DPostCode,
                DState = obj.DState,
                Email = obj.Email,
                Name = obj.Name,
                PostCode = obj.PostCode,
                State = obj.State,
                orderID = Convert.ToInt32(Session["OrderId"])
            });
            if (check)
            {
                TempData["Message"] = "Saved Successfully";
                // Take reference from other web site 
            }
            else
            {
                TempData["Message"] = "Error! Unable to save";
                // Take reference from other web site 
            }
            return RedirectToAction("IPIN","Payment");
        }


        [HttpPost]
        public ActionResult Valdate(LoginModel obj)
        {
            obj.isadmin = false;
            TempData["Message"] = "";
            var check = UserDal.ValidateUser(obj);
            if (check != null)
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
                return RedirectToAction("index", "Checkout", new { id = check.id });
                // Take reference from other web site 
            }
            else
            {
                TempData["Invalidlogin"] = "Invalid Username and password";
                // Take reference from other web site 
            }
            return RedirectToAction("Index");

        }

    }
}
