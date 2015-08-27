using Ecommerce.DataAccess;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class PaymentController : Controller
    {
        //
        // GET: /Payment/

        public ActionResult Index(int id)
        {
            ViewBag.Image = "http://testing.issoftwaresolution.in/img/Logo.png";
            ViewBag.Thanks = "http://testing.issoftwaresolution.in/img/thanks.jpg";
            var order = OrderDal.GetByOrderId(id);
            return View(order);
        }

        public ActionResult IPIN()
        {
            int orderId = Convert.ToInt32(Session["OrderId"]);
            var order = OrderDal.GetByOrderId(orderId);
            return View(order);
        }

        public ActionResult PreStatus()
        {
           Session["st"]= Convert.ToString(Request.QueryString["st"]);
           Session["tx"] = Convert.ToString(Request.QueryString["tx"]);
           return View();
        }

        public ActionResult Status()
        {
            int orderId = Convert.ToInt32(Session["OrderId"]);
            string url = "";
            try
            {
                HttpCookie cookie = Request.Cookies["OrderId"];
                if (cookie != null)
                {
                    orderId = Convert.ToInt32(cookie.Value);
                }
                OrderDal.UpdatePaymentStatusInOrder(orderId, Convert.ToString(Session["st"]), Convert.ToString(Session["tx"]));
                    var order = OrderDal.GetByOrderId(orderId);
                    url = "http://testing.issoftwaresolution.in/payment/index/" + orderId;
                    string emailMessage = Utility.GetResponse(url);
                    Utility.SendEmail(order.Email, emailMessage, "Order confirmation", ConfigurationManager.AppSettings["WebsiteUrl"].ToString());
                    Session["Cart"] = new CartModel();
                    return View(order);
                
            }
            catch(Exception ex)
            {
                ViewBag.Cookies = "";
                HttpCookie cookie = Request.Cookies["OrderId"];
                if (cookie != null)
                {
                    orderId = Convert.ToInt32(cookie.Value);
                }
                
              ViewBag.url = url;
              ViewBag.Error=ex.InnerException;
              ViewBag.Message = ex.Message;
              ViewBag.StakeTrace=ex.TargetSite+" ------   "+ex.StackTrace ;

              ViewBag.orderId = orderId;

              ViewBag.SessionorderId = orderId;
              return View("Test");
            }

        }
        public ActionResult OrderDetail()
        {
            if (Request.QueryString["orderId"] != null)
            {
                var orderId = Convert.ToInt32(Request.QueryString["orderId"]);
                var order = OrderDal.GetByOrderId(orderId);
                ViewBag.history = "true";
                return View("Status",order);
            }
            ViewBag.Error = "Error processing request";
            ViewBag.Message = "";
            ViewBag.StakeTrace ="";
            ViewBag.url = "";
            ViewBag.orderId = "";
            return View("Test");
        }
        public ActionResult NotifyFromPaypal()
        {
            return null;
        }

    }
}
