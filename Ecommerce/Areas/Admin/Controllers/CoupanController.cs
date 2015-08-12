using Ecommerce.DataAccess;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class CoupanController : BaseController
    {
        //
        // GET: /Admin/Coupan/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.PageTittle = "Category";
                ViewBag.breadCrum = "<a href='/Admin/Category/index'>Category</a>";
                List<Model.CoupanModel> list = new List<Model.CoupanModel>();
                list = CoupanDal.GetAll().ToList();
                return View(list);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.PageTittle = "Add Coupan";
                CoupanModel obj = new CoupanModel();
                return View(obj);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        public ActionResult Edit(int id)
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.PageTittle = "Edit Coupan";

                CoupanModel obj = new CoupanModel();
                obj = CoupanDal.GetById(id);
                return View("Create", obj);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        public ActionResult Save(CoupanModel obj)
        {
            bool check = true;
            if (Request.IsAuthenticated)
            {
                if (obj.id > 0)
                {
                    check = CoupanDal.Update(obj);
                }
                else
                {
                    check = CoupanDal.Create(obj);
                }
                if (check)
                {
                    TempData["message"] = "Saved successfully";
                }
                else
                {
                    TempData["message"] = "Error while saving data";
                }
                return RedirectToAction("Create", "Category");
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        public ActionResult Delete(int Id)
        {
            bool check = true;
            check = CoupanDal.Delete(Id);
            return Json(check,JsonRequestBehavior.AllowGet);
        }

    }
}
