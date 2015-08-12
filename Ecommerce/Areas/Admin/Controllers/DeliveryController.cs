using Ecommerce.DataAccess;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class DeliveryController : BaseController
    {
        //
        // GET: /Admin/Delivery/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                DeliveryModel obj = new DeliveryModel();
                obj = DeliveryDal.Get();
                return View(obj);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        [HttpPost]
        public ActionResult Save(DeliveryModel obj)
        {
            bool check = true;
            if (Request.IsAuthenticated)
            {
                check = DeliveryDal.Update(obj);
                if (check)
                {
                    TempData["message"] = "Saved successfully";
                }
                else
                {
                    TempData["message"] = "Error while saving data";
                }
                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

    }
}
