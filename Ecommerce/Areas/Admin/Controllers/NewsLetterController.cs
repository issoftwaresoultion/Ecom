using Ecommerce.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class NewsLetterController : Controller
    {
        //
        // GET: /Admin/NewsLetter/

        public ActionResult Index()
        {
            return View();
        }
        [ValidateInput(false)] 
        public ActionResult send(Ecommerce.DbEntity.staticpage obj)
        {
            var email = NewLetter.GetAllEmail();
            foreach (var x in email)
            {
                Utility.SendEmail(x.email, obj.Content, obj.Heading, ConfigurationManager.AppSettings["WebsiteUrl"].ToString());
            }
            return Json("You request to send newsletter will be processed");
        }

    }
}
