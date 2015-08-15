using Ecommerce.DataAccess;
using System;
using System.Collections.Generic;
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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IPIN()
        {
            int orderId = Convert.ToInt32(Session["OrderId"]);
            var order = OrderDal.GetByOrderId(orderId);
            return View(order);
        }

         public ActionResult RedirectFromPaypal()
        {
            ViewBag.Text = Response; 
            return View();
        }
         public ActionResult Status()
        {
            
            using (Stream input = Response.OutputStream)
            {
                ViewBag.Text = input.ToString(); 
            }
             
            
            return View();
        }
         public ActionResult NotifyFromPaypal()
        {
            return null;
        }
    }
}
