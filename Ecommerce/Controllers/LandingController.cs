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
    public class LandingController : FrontBaseController
    {
        //
        // GET: /Home/

        public ActionResult ChangeCurrency(string id)
        {
            Session["currency"] = id;
            Session["Cart"] = CartDal.UpdateCurrencyChange((CartModel)Session["Cart"], (string)Session["currency"]);
            return RedirectToAction("index");
        }

        public ActionResult Index(string id)
        {

            if (Session["currency"] == null)
            {
                Session["currency"] = "Dollar";
            }

            ViewBag.Hompage = "";
            ViewBag.Productmodel = new List<ProductModel>(); 
            if (String.IsNullOrEmpty(id))
            {
                Session["pageType"] = "home";
                ViewBag.breadcrums="<ol class='breadcrumb'><li><a href='#'>Home</a></li></ol>";
                ViewBag.Hompage = "true";
                var hotproduct =ProductDal.GetHotProduct();
                foreach(var x in hotproduct)
                {
                 x.SelectedLengthList= LengthDal.GetAllLengthByProductId(x.Id).Where(m => m.id > 0).Distinct().ToList();
                 
                 x.SelectedColorList = ColorDal.GetColorByProductandLength(x.Id, x.SelectedLengthList[0].id).ToList();
                }
                ViewBag.Productmodel = hotproduct; 
            }
            else
            {
                Session["pageType"] = id;
                int category = CategoryDal.GetByCode(id).id;
                var Productmodel = ProductDal.GetByProductCategory(category);
                ViewBag.breadcrums = "<ol class='breadcrumb'><li><a href='/Landing/Index'>Home</a></li><li>" + id + "</li></ol>";
                foreach (var x in Productmodel)
                {
                    x.SelectedLengthList = LengthDal.GetAllLengthByProductId(x.Id).Where(m => m.id > 0).Distinct().ToList();
                    x.SelectedLengthList.Add(new LengthModel { id = 0, value = "Length" });
                    x.SelectedColorList = ColorDal.GetColorByProductandLength(x.Id, x.SelectedLengthList[0].id).ToList();
                    x.SelectedColorList.Add(new ColorModel { id = 0, name = "Color" });
                }
                ViewBag.Productmodel = Productmodel;
            }
            return View();
        }

        public ActionResult GetColorDropdown()
        {
            var productId = Convert.ToInt32(Request.QueryString["productId"]);
            var LengthId = Convert.ToInt32(Request.QueryString["lengthId"]);
            var color = ColorDal.GetColorByProductandLength(productId, LengthId);
            return Json(color, JsonRequestBehavior.AllowGet);
            
        }
        public ActionResult GetPrice()
        {
            var productId = Convert.ToInt32(Request.QueryString["productId"]);
            var LengthId = Convert.ToInt32(Request.QueryString["lengthId"]);
            var colorId = Convert.ToInt32(Request.QueryString["colorId"]);
            if (Session["currency"] == null)
            {
                Session["currency"] = "Dollar";
            }
            var Price = ProductPricingDal.GetPriceByProductandLengthandColor(productId, LengthId, colorId,  Convert.ToString(Session["currency"]));
            if (Price.Id > 0)
            {
                return Json(Price, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SignOut()
        {
            Logout();
            Session.Abandon();
            return RedirectToAction("Index", "Landing");
        }
        [HttpPost]
        public ActionResult Validate(LoginModel obj)
        {
            obj.isadmin = false;
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
                return RedirectToAction("Index", "Landing");
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid Username and password";
                return RedirectToAction("Index", "Checkout");
            }
        }
    }
}
