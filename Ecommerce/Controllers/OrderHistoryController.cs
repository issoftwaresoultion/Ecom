using Ecommerce.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class OrderHistoryController : FrontBaseController
    {
        //
        // GET: /OrderHistory/

        public ActionResult Index()
        {
            var LoginUser = GetLoginUserdata();
            var Model = OrderDal.GetOrderUserId(LoginUser.id);
            return View(Model);
        }

    }
}
