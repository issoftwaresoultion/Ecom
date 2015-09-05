using Ecommerce.DataAccess;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class OrderManagementController : Controller
    {
        //
        // GET: /Admin/OrderManagement/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderList()
        {
            var model = new List<OrderHeader>();
            if (Request.Form["DDStatus"] == null)
            {
                model = OrderDal.GetOrdersByStatus("Pending", 0);
            }
            else
            {
                model = OrderDal.GetOrdersByStatus(Convert.ToString(Request.Form["DDStatus"]), 0);
            }
            return View("OrderList", model);
        }
        public ActionResult GetByOrderId()
        {
            int id = 0;
            var model = new List<OrderHeader>();
            if (Request.Form["orderId"] != null)
            {
                id = Convert.ToInt32(Request.Form["orderId"]);
            }
            model = OrderDal.GetOrdersByStatus("", id);
            return View("OrderList", model);
        }
        public ActionResult GetOrderDetail(int id)
        {
            var order = OrderDal.GetByOrderId(id);
            return View(order);
        }
        [HttpPost]
        public ActionResult Update(OrderHeader obj )
        {
            string orderStatus=Request.Form["OrderStatus"].ToString();
            if (orderStatus == "Dispatched")
            {
                string url = "http://localhost:2585/payment/DispatchEmail/" + obj.orderID;
                //string url = "http://testing.issoftwaresolution.in/payment/DispatchEmail/" + obj.orderID;
                string emailMessage = Utility.GetResponse(url);
                Utility.SendEmail(obj.Email, emailMessage, "Order Dispatch", ConfigurationManager.AppSettings["WebsiteUrl"].ToString());
            }
            OrderDal.UpdateOrderStatus(obj.orderID, obj.TrackingNumber, orderStatus);
            return RedirectToAction("GetOrderDetail", new { id = obj.orderID});
        }

    }
}
