using Ecommerce.DataAccess;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        //
        // GET: /Admin/Category/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            { 
                ViewBag.PageTittle = "Category";
                ViewBag.breadCrum = "<a href='/Admin/Category/index'>Category</a>";
                List<Model.ExtendedCategory> listCategory = new List<Model.ExtendedCategory>();
                listCategory = CategoryDal.GetAllCategory().Where(m=>m.id!=0).ToList();
               
                return View(listCategory);
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
                ViewBag.PageTittle = "Add Category";
                ViewBag.breadCrum = "<a href='/Admin/Category/index'>Category</a> >> Add Category";
                ExtendedCategory obj = new ExtendedCategory();
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
                ViewBag.PageTittle = "Edit Category";
                ViewBag.breadCrum = "<a href='/Admin/Category/index'>Category</a> >> Edit Category";
                ExtendedCategory obj = new ExtendedCategory();
                obj = CategoryDal.GetById(id);
                return View("Create", obj);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        public ActionResult Save(ExtendedCategory obj)
        {
          bool check = true;
        if (Request.IsAuthenticated)
            {
                if (obj.id > 0)
                {
                    check = CategoryDal.Update(obj);
                }
                else
                {
                    check=CategoryDal.Create(obj);
                }
                if (check)
                {
                    TempData["message"] = "Saved successfully";
                }
                else
                {
                    TempData["message"] = "Error while saving data";
                }
                return RedirectToAction("Create","Category");
            }
        else
        {
            return RedirectToAction("index", "home");
        }
        }
        public ActionResult Delete(int Id)
        {
          bool check = true;
          check = CategoryDal.Delete(Id);
          return Json(check, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Past(int Id)
        {
            if (Session["copyProduct"] != null)
            {
                string NewFilename = Guid.NewGuid().ToString();
                
                ProductModel obj = (ProductModel)Session["copyProduct"];
                string fileextention = Path.GetExtension(obj.Image);
                NewFilename = NewFilename.ToString() + fileextention;
                System.IO.File.Copy(Path.Combine(Server.MapPath("~/ProductImages/") , obj.Image), Path.Combine(Server.MapPath("~/ProductImages/") , NewFilename));
                obj.Image = NewFilename;
                obj.CatId = Id;
                obj.Id = 0;
                List<ColorModel> colorList = new List<ColorModel>();

                foreach (var x in obj.ProductPrice)
                {
                    colorList.Add(new ColorModel
                    {
                    id=x.colorId,
                    IsSelected=true,
                    LengthId=x.lengthId,
                    ourpriceangola=x.ourpriceangola,
                    OurPriceDollar=x.OurPriceDollar,
                    OurPriceEuro=x.OurPriceEuro,
                    ourpriceghana=x.ourpriceghana,
                    ourpricenigeria=x.ourpricenigeria,
                    OurPricePound=x.OurPricePound,
                    priceangola=x.priceangola,
                    PriceDollar=x.PriceDollar,
                    PriceEuro=x.PriceEuro,
                    priceghana=x.priceghana,
                    pricenigeria=x.pricenigeria,
                    PricePound=x.PricePound,
                   
                    
                    });
                }
                obj.ColorList = colorList;
               ProductDal.Create(obj);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
            }
    }
}
