using Ecommerce.DataAccess;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class CartController : FrontBaseController
    {
        //
        // GET: /Cart/

        public ActionResult Checkout()
        {
            if (Session["currency"] == null)
            {
                Session["currency"] = "Dollar";
            }
            var obj = (CartModel)Session["Cart"];

            if (obj == null)
            {
                return RedirectToAction("Index", "Landing");
            }
            return View();
        }
        public ActionResult CheckoutPartial()
        {
            if (Session["currency"] == null)
            {
                Session["currency"] = "Dollar";
            }
            var obj = (CartModel)Session["Cart"];
            ViewBag.DeliveryDays = DeliveryDaysDal.Get().days;
            return PartialView("_CheckOutPartial", obj);
        }
        public ActionResult MiniIndex()
        {
            var obj = (CartModel)Session["Cart"];
            if (obj != null)
            {
                if (obj.Product != null)
                {
                    ViewBag.count = obj.Product.Count();
                }
                else
                {
                    ViewBag.count = 0;
                }
            }
            else
            {
                ViewBag.count = 0;
            }
            return PartialView(obj);
        }
        public ActionResult Ordersummery()
        {
            CartModel obj = new CartModel();
            obj.Product = new List<ProductModel>();
            var order = OrderDal.GetByOrderId((int)Session["OrderId"]);
            obj.Total = order.AmountInCurrencyChoosenByuser;
            obj.DelivierCharges = order.DeliveryCharges;
            foreach (var x in order.OrderDetail)
            {
                var productPrice = new ProductPriceModel();
                productPrice = ProductPricingDal.GetPriceByProductPriceId(x.ProductPriceId, Convert.ToString(Session["currency"]));
                var product = new ProductModel();
                product = ProductDal.GetById(productPrice.ProductId);
                var productModel = new ProductModel();
                productModel.Image = product.Image;
                productModel.Name = product.Name;
                productModel.Price = new ProductPriceModel();
                productModel.Price.Ourprice = x.ActualPriceInUserSeletedCurrency;
                productModel.Price.Quantity = x.Quantity;
                productModel.Price.LengthName = productPrice.LengthName;
                productModel.Price.ColorName = productPrice.ColorName;

                obj.Product.Add(productModel);
            }

            return PartialView(obj);
        }

        public ActionResult AddToCart(int id)
        {
            if (Session["currency"] == null)
            {
                Session["currency"] = "Dollar";
            }
            Session["Cart"] = CartDal.AddItemTocart(id, (CartModel)Session["Cart"], (string)Session["currency"]);
            return RedirectToAction("MiniIndex");
            //return Json(true,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete()
        {
            if (Session["currency"] == null)
            {
                Session["currency"] = "Dollar";
            }
            int rowid = Convert.ToInt32(Request.QueryString["countid"]);
            Session["Cart"] = CartDal.Delete((CartModel)Session["Cart"], (string)Session["currency"], rowid);
            return RedirectToAction("MiniIndex");
        }
        //deleteing from the main cart 
        public ActionResult DeleteItemFromCheckOutCart()
        {
            if (Session["currency"] == null)
            {
                Session["currency"] = "Dollar";
            }
            int rowid = Convert.ToInt32(Request.QueryString["countid"]);
            Session["Cart"] = CartDal.Delete((CartModel)Session["Cart"], (string)Session["currency"], rowid);
            return RedirectToAction("CheckoutPartial");
        }

        public ActionResult UpdateCoupan(string id)
        {
            if (Session["currency"] == null)
            {
                Session["currency"] = "Dollar";
            }

            var cart = (CartModel)Session["Cart"]; //= CartDal.AddItemTocart(id, (CartModel)Session["Cart"], (string)Session["currency"]);
            var validate = CoupanDal.ValidateCoupan(id);
            if (validate != null)
            {
                cart.DiscountCoupan = id;
                cart.DiscountAmmount = (cart.SubTotalTotal) * (validate.DiscountPercentage / 100);
                cart.Total = cart.Total - cart.DiscountAmmount;
                cart.Total = Math.Round(cart.Total, 2);
                cart.DiscountAmmount = Math.Round(cart.DiscountAmmount, 2);
            }
            Session["Cart"] = cart;
            return RedirectToAction("CheckoutPartial");
            //return Json(check, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidateCoupan(string id)
        {
            if (Session["currency"] == null)
            {
                Session["currency"] = "Dollar";
            }

            var cart = (CartModel)Session["Cart"]; //= CartDal.AddItemTocart(id, (CartModel)Session["Cart"], (string)Session["currency"]);
            var validate = CoupanDal.ValidateCoupan(id);
            if (validate != null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }


    }
}
