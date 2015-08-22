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
                List<DeliveryModel> obj = new List<DeliveryModel>();
                obj = DeliveryDal.GetAll();
                for (int i = 0; i < 10; i++)
                {
                    if (obj.Count == 0)
                    {
                        obj.Add(new DeliveryModel
                        {
                            Active = true,
                            Default = true,
                            freeDeliveryAmountAngola = 0,
                            freeDeliveryAmountDoller = 0,
                            freeDeliveryAmountEuro = 0,
                            freeDeliveryAmountGhana = 0,
                            freeDeliveryAmountNigria = 0,
                            freeDeliveryAmountPound = 0,
                            DeliveryDays="Default",
                        });
                    }

                    if (obj.Count < 5)
                    {
                        obj.Add(new DeliveryModel
                        {
                            Active = false,
                            Default = false,
                            freeDeliveryAmountAngola = 0,
                            freeDeliveryAmountDoller = 0,
                            freeDeliveryAmountEuro = 0,
                            freeDeliveryAmountGhana = 0,
                            freeDeliveryAmountNigria = 0,
                            freeDeliveryAmountPound = 0
                        });
                    }
                    else
                    {
                        break;
                    }

                }
                return View(obj);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        [HttpPost]
        public ActionResult Save(List<DeliveryModel> obj)
        {
            bool check = true;
            if (Request.IsAuthenticated)
            {
                foreach (var x in obj)
                {
                    if (x.id > 0)
                    {
                        check = DeliveryDal.Update(x);
                    }
                    else
                    {
                        check = DeliveryDal.Create(x);
                    }
                    if (!check)
                    {
                        break;
                    }
                }
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
