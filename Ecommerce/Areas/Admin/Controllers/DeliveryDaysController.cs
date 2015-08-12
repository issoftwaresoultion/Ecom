using Ecommerce.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class DeliveryDaysController : Controller
    {
        //
        // GET: /Admin/DeliveryDays/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var obj = DeliveryDaysDal.Get();
                return View(obj);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
         
        }


        [HttpPost]
        public ActionResult Save(Ecommerce.DbEntity.deliveryday obj)
        {
            bool check = true;
            if (Request.IsAuthenticated)
            {
                check = DeliveryDaysDal.Update(obj);
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
