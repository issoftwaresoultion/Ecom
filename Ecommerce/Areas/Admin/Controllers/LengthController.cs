using Ecommerce.DataAccess;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class LengthController : Controller
    {
        //
        // GET: /Admin/Length/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.PageTittle = "Length";
                ViewBag.breadCrum = "<a href='/Admin/Length/index'>Length</a>";
                List<Model.LengthModel> listColor = new List<Model.LengthModel>();
                listColor = LengthDal.GetAlllength().Where(m => m.id != 0).ToList();
                return View(listColor);
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
                ViewBag.PageTittle = "Add Length";
                ViewBag.breadCrum = "<a href='/Admin/Length/index'>Length</a> >> Add Length";
                LengthModel obj = new LengthModel();
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
                ViewBag.PageTittle = "Edit Length";
                ViewBag.breadCrum = "<a href='/Admin/Length/index'>Length</a> >> Edit Length";
                LengthModel obj = new LengthModel();
                obj = LengthDal.GetById(id);
                return View("Create", obj);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        public ActionResult Save(LengthModel obj)
        {
            bool check = true;
            if (Request.IsAuthenticated)
            {
                if (obj.id > 0)
                {
                    check = LengthDal.Update(obj);
                }
                else
                {
                    check = LengthDal.Create(obj);
                }
                if (check)
                {
                    TempData["message"] = "Saved successfully";
                }
                else
                {
                    TempData["message"] = "Error while saving data";
                }
                return RedirectToAction("Create", "Length");
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        public ActionResult Delete(int Id)
        {
            //bool check = true;
            //check = CategoryDal.Delete(Id);
            //return Json(check);
            return null;
        }

    }
}
