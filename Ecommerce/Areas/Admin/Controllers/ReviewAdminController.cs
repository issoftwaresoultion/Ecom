using Ecommerce.DataAccess;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class ReviewAdminController : Controller
    {
        //
        // GET: /Admin/ReviewAdmin/

        public ActionResult Index(int Id)
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.PageTittle = "Add Review";
                //ViewBag.breadCrum = "<a href='/Admin/Length/index'>Length</a> >> Add Length";
                ViewBag.PageTittle = "Review";
                ViewBag.productId = Id;
                var list = ReviewDal.GetAllbyProductId(Id);
                return View(list);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        public ActionResult Create(int id)
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.PageTittle = "Add Review";
                //ViewBag.breadCrum = "<a href='/Admin/Length/index'>Length</a> >> Add Length";
                Dictionary<int, string> list = new Dictionary<int, string>();
                list.Add(1, "Very bad");
                list.Add(2, "Not that bad");
                list.Add(3, "Average");
                list.Add(4, "Good");
                list.Add(5, "Perfect");
                ViewBag.list = list;
                ReviewModel obj = new ReviewModel();
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
                Dictionary<int, string> list = new Dictionary<int, string>();
                list.Add(1, "Very bad");
                list.Add(2, "Not that bad");
                list.Add(3, "Average");
                list.Add(4, "Good");
                list.Add(5, "Perfect");
                ViewBag.list = list;
               // ViewBag.PageTittle = "Edit Length";
                //ViewBag.breadCrum = "<a href='/Admin/Length/index'>Length</a> >> Edit Length";
                ReviewModel obj = new ReviewModel();
                obj = ReviewDal.GetById(id);
                return View("Create", obj);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        public ActionResult Save(ReviewModel obj)
        {
            bool check = true;
            if (Request.IsAuthenticated)
            {
                if (obj.id > 0)
                {
                    check = ReviewDal.Update(obj);
                }
                else
                {
                    check = ReviewDal.Create(obj);
                }
                if (check)
                {
                    TempData["message"] = "Saved successfully";
                }
                else
                {
                    TempData["message"] = "Error while saving data";
                }
                return RedirectToAction("Create", "ReviewAdmin");
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

    }
}
