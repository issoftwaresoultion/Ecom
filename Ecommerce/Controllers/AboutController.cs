using Ecommerce.DataAccess;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class AboutController : Controller
    {
        //
        // GET: /About/

        public ActionResult Index(string id)
        {
            ViewBag.pageTitle = id;
            if (id == "ReturnPolicy")
            {
                id = "Return Policy";
                ViewBag.pageTitle = "Return Policy";
            }
            else if (id == "Conditions")
            {
                ViewBag.pageTitle = "Terms and Conditions";
            }
            var obj = StaticPagesDal.GetByHeading(id);
            return View(obj);
        }
        
        public ActionResult ContactUs()
        {
            var obj = new ContactUs();
            return View(obj);
        }

        [HttpPost]
        public ActionResult SendContactUs(ContactUs obj)
        {
            string message = obj.Email + System.Environment.NewLine + "Email-" + obj.Email + System.Environment.NewLine + "Contact-" + obj.ContactNumber + System.Environment.NewLine + "Name-" + obj.Name;
            Utility.SendEmail("indub@speedexam.in", message, obj.Subject, obj.Name);
            TempData["Message"] = "Your query has been submitted successfully.";
            return RedirectToAction("ContactUs");
        }
    }
}
