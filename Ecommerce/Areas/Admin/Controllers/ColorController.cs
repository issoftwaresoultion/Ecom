using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.DataAccess;
using Ecommerce.Model;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class ColorController : BaseController
    {
        //
        // GET: /Admin/Color/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.PageTittle = "Color";
                ViewBag.breadCrum = "<a href='/Admin/Color/index'>Color</a>";
                List<Model.ColorModel> listColor = new List<Model.ColorModel>();
                listColor = ColorDal.GetAllColor().Where(m => m.id != 0).ToList();
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
                ViewBag.PageTittle = "Add Color";
                ViewBag.breadCrum = "<a href='/Admin/Color/index'>Category</a> >> Add Color";
                ColorModel obj = new ColorModel();
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
                ViewBag.PageTittle = "Edit Color";
                ViewBag.breadCrum = "<a href='/Admin/Color/index'>Color</a> >> Edit Color";
                ColorModel obj = new ColorModel();
                obj = ColorDal.GetById(id);
                return View("Create", obj);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        public ActionResult Save(ColorModel obj)
        {
            bool check = true;
            if (Request.IsAuthenticated)
            {
                if (obj.id > 0)
                {
                    check = ColorDal.Update(obj);
                }
                else
                {
                    check = ColorDal.Create(obj);
                }
                if (check)
                {
                    TempData["message"] = "Saved successfully";
                }
                else
                {
                    TempData["message"] = "Error while saving data";
                }
                return RedirectToAction("Create", "Color");
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
