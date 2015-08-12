using Ecommerce.DataAccess;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class ProductsController : FrontBaseController
    {
        //
        // GET: /Products/

        public ActionResult Index(int id)
        {
            if (Session["currency"] == null)
            {
                Session["currency"] = "Dollar";
            }

            ProductModel obj = ProductDal.GetByProductByProductId(id);
            obj.SelectedLengthList = LengthDal.GetAllLengthByProductId(obj.Id).Where(m => m.id > 0).Distinct().ToList();
            obj.SelectedColorList = ColorDal.GetColorByProductandLength(obj.Id, obj.SelectedLengthList[0].id).ToList();
            ViewBag.relatedProduct = ProductDal.GetRelatedProducts(obj.KeyWord,id);
            if (Convert.ToString(Session["pageType"]) == "home")
            {
                ViewBag.breadcrums = "<ol class='breadcrumb'><li><a href='/Landing/Index'>Home</a></li><li>" + obj.Name + "</li></ol>";
            }
            else
            {
                ViewBag.breadcrums = "<ol class='breadcrumb'><li><a href='/Landing/Index'>Home</a></li><li><a href='/Landing/home/" + CategoryDal.GetById(obj.CatId).Code + "'>" + obj.Category + "</a></li><li>" + obj.Name + "</li></ol>";
            }
                return View(obj);
        }

        public ActionResult ProductReview(int id)
        {
            if (Session["currency"] == null)
            {
                Session["currency"] = "Dollar";
            }
            var obj = new ReviewModel();
            obj.ProductId = id;
            Dictionary<int, string> list = new Dictionary<int, string>();
            list.Add(5, "Perfect");
            list.Add(4, "Good");
            list.Add(3, "Average");
            list.Add(2, "Not that bad");
            list.Add(1, "Very bad");
            ViewBag.list = list;
            ViewBag.Reviews = ReviewDal.GetAllbyProductId(id);

            return View(obj);
        }


        public ActionResult Save(ReviewModel obj)
        {
          var  check = ReviewDal.Create(obj);
          return Json(check);
        }
    }
}
