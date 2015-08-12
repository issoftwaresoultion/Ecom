using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Model;
using Ecommerce.DataAccess;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /Admin/User/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.PageTittle = "Category";
                ViewBag.breadCrum = "<a href='/Admin/Category/index'>Category</a>";
                List<Model.adminUser> list = new List<Model.adminUser>();
                list = AdminUserDal.GetAll().ToList();
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
                ViewBag.PageTittle = "Add User";
                ViewBag.breadCrum = "<a href='/Admin/Category/index'>Category</a> >> Add Category";
                adminUser obj = new adminUser();
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
                ViewBag.PageTittle = "Edit User";
                ViewBag.breadCrum = "<a href='/Admin/Category/index'>Category</a> >> Edit Category";
                adminUser obj = new adminUser();
                obj = AdminUserDal.GetById(id);
                return View("Create", obj);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        public ActionResult Save(adminUser obj)
        {
            bool check = true;
            if (Request.IsAuthenticated)
            {
                if (obj.id > 0)
                {
                    check = AdminUserDal.Update(obj);
                }
                else
                {
                    check = AdminUserDal.Create(obj);
                }
                if (check)
                {
                    TempData["message"] = "Saved successfully";
                }
                else
                {
                    TempData["message"] = "Error while saving data";
                }
                return RedirectToAction("Create", "User");
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        public ActionResult Delete(int Id)
        {
            bool check = true;
            check = AdminUserDal.Delete(Id);
            return Json(check, JsonRequestBehavior.AllowGet);
        }

    }
}
