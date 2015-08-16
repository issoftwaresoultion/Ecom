using Ecommerce.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.DbEntity;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class AdminStaticpageController : Controller
    {
        //
        // GET: /Admin/AdminStaticpage/

        public ActionResult Index()
        {
            var obj = StaticPagesDal.GetAll();
            return View(obj);
        }

        public ActionResult Create(int id)
        {
            var obj = new staticpage();
            if (id > 0)
            {
                obj = StaticPagesDal.GetById(id);
            }

            return View(obj);
        }
       [ValidateInput(false)] 
        public ActionResult Save(staticpage obj)
        {
            bool check = true;
            if (Request.IsAuthenticated)
            {
                if (obj.id > 0)
                {
                    check = StaticPagesDal.Update(obj);
                }
                else
                {
                    check = StaticPagesDal.Create(obj);
                }
                if (check)
                {
                    TempData["message"] = "Saved successfully";
                }
                else
                {
                    TempData["message"] = "Error while saving data";
                }
                return RedirectToAction("Create", "AdminStaticpage", new  {id=0 });
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }


    }
}
