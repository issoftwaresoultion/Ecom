using Ecommerce.DataAccess;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class BrandController : BaseController
    {
        //
        // GET: /Admin/Brand/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.PageTittle = "Brand";
                ViewBag.breadCrum = "<a href='/Admin/Brand/index'>Brand</a>";
                List<Model.BrandModel> listBrand = new List<Model.BrandModel>();
                listBrand = BrandDal.GetAllBrand();

                return View(listBrand);
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
                ViewBag.PageTittle = "Add Brand";
                ViewBag.breadCrum = "<a href='/Admin/Brand/index'>Brand</a> >> Add Brand";
                BrandModel obj = new BrandModel();
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
                //ViewBag.PageTittle = "Edit Brand";
                //ViewBag.breadCrum = "<a href='/Admin/Brand/index'>Brand</a> >> Edit Brand";
                //BrandModel obj = new BrandModel();
                //obj = BrandDal.GetById(id);
                return View("Create");
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        public ActionResult Save(BrandModel obj)
        {
            bool check = true;
            if (Request.IsAuthenticated)
            {
                if (obj.id > 0)
                {
                    check = BrandDal.Update(obj);
                }
                else
                {
                    check = BrandDal.Create(obj);
                }
                if (check)
                {
                    TempData["message"] = "Saved successfully";
                }
                else
                {
                    TempData["message"] = "Error while saving data";
                }
                return RedirectToAction("Create", "Brand");
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

    }
}
