using Ecommerce.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class ShareController : FrontBaseController
    {
        //
        // GET: /Shared/

        public ActionResult MainMenu()
        {
            var caterGory = CategoryDal.GetAllCategory().ToList();
            caterGory = caterGory.Where(m => m.id > 0).ToList();
            
            return View("MainMenu",caterGory);
        }

       

    }
}
