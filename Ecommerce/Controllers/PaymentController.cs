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
            ViewBag.Image = ConfigurationManager.AppSettings["WebsiteUrl"].ToString() + "/img/Logo.png";
            var order = OrderDal.GetByOrderId(id);
            return View(order);
        }

        public ActionResult IPIN()
        {
            int orderId = Convert.ToInt32(Session["OrderId"]);
            var order = OrderDal.GetByOrderId(orderId);
            return View(order);
        }


        public ActionResult Status()
        {
            int orderId = Convert.ToInt32(Session["OrderId"]);
            OrderDal.UpdatePaymentStatusInOrder(orderId, Convert.ToString(Request.QueryString["st"]), Convert.ToString(Request.QueryString["tx"]));
            var order = OrderDal.GetByOrderId(orderId);
            string url = ConfigurationManager.AppSettings["WebsiteUrl"].ToString() + "/payment/index/" + orderId;
            string emailMessage = Utility.GetResponse(url);
            Utility.SendEmail(order.Email, emailMessage, "Order confirmation", ConfigurationManager.AppSettings["WebsiteUrl"].ToString());
            Session["OrderId"] = "";
            Session["Cart"] = new CartModel();
            return View(order);
        }
        public ActionResult NotifyFromPaypal()
        {
            return null;
        }

    }
}
