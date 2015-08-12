using Ecommerce.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Model;
using System.IO;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        //
        // GET: /Admin/Product/

        public ActionResult IndexFromCategory(int id)
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.PageTittle = "Products";
                ViewBag.Id = id;
                ViewBag.Type = "cat";
                var catName = CategoryDal.GetById(id).name;
                var Product = ProductDal.GetByProductCategory(id);
                ViewBag.breadCrum = "<a href='/Admin/Category/index'>" + catName + "</a> >> Products ";
                return View("Index", Product);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        //public ActionResult IndexFromBrand(int id)
        //{
        //    if (Request.IsAuthenticated)
        //    {
        //        ViewBag.PageTittle = "Products";
        //        ViewBag.Id = id;
        //        ViewBag.Type = "cat";
        //        var brandName = BrandDal.GetById(id).BrandName;
        //        var Product = ProductDal.GetByProducBrand(id);
        //        ViewBag.breadCrum = "<a href='/Admin/Brand/index'>" + brandName + "</a> >> Products ";
        //        return View("Index",Product);
        //    }
        //    else
        //    {
        //        return RedirectToAction("index", "home");
        //    }
        //}
        public ActionResult Create(string id)
        {
            var product = new ProductModel();
            product.ProductPrice = new List<ProductPriceModel>();
            //for(int i=0;i<5;i++)
            //{
            //    product.ProductPrice.Add(new ProductPriceModel());
            //}
            product.brandList = BrandDal.GetAllBrand();
            product.CategoryList = CategoryDal.GetAllCategory();
            product.ColorList = ColorDal.GetAllColor().Where(m => m.id != 0).ToList();
            product.LengthList = LengthDal.GetAlllength().Where(m => m.id != 0).ToList();
            string name = "";
            string type = "";
            int Id = 0;
            if (id != null)
            {
                Id = Convert.ToInt32(id);
            }

            if (Request.QueryString["tId"] != null)
            {
               Id= Convert.ToInt32(Request.QueryString["tId"]);
            }
            if (Request.QueryString["type"] != null)
            {
              type=  Convert.ToString(Request.QueryString["type"]);
            }
            ViewBag.RequestType = type;
            if (Request.IsAuthenticated)
            {
                ViewBag.PageTittle = "Add Product";
                if (type == "cat")
                {
                    name = CategoryDal.GetById(Id).name;
                    ViewBag.breadCrum = "<a href='/Admin/Category/index'>Category</a> >> <a href='/Admin/Product/IndexFromCategory/" + Id + "'>" + name + "</a> >> Add Product";
                    product.CatId = Id;
                }
                else
                {
                    //name = BrandDal.GetById(Id).BrandName; 
                    ViewBag.breadCrum = "<a href='/Admin/Brand/index'>Brand</a> >> <a href='/Admin/Product/IndexFromBrand/" + Id + "'>" + name + "</a> >> Add Product";
                    product.BrandId = Id;
                }
                product.SelectedColorList = new List<ColorModel>();//ColorDal.GetAllColorsByProductId(Id);
                product.SelectedLengthList = new List<LengthModel>();//LengthDal.GetAllLengthByProductId(Id);
                return View(product);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        public ActionResult Edit()
        {
             
            string name = "";
            string type = "";
            int Id = 0;
            if (Request.QueryString["tId"] != null)
            {
                Id = Convert.ToInt32(Request.QueryString["tId"]);
            }
            if (Request.QueryString["type"] != null)
            {
                type = Convert.ToString(Request.QueryString["type"]);
            }
            ViewBag.RequestType = type;
            var product = ProductDal.GetById(Id);
            //for (int i = 0; i < 5; i++)
            //{
            //    product.ProductPrice.Add(new ProductPriceModel());
            //}
            product.brandList = BrandDal.GetAllBrand();
            product.CategoryList = CategoryDal.GetAllCategory();
            product.ColorList = ColorDal.GetAllColor().Where(m => m.id != 0).ToList();
            product.LengthList = LengthDal.GetAlllength().Where(m => m.id != 0).ToList();
            product.SelectedColorList = ColorDal.GetAllColorsByProductIdForEditProduct(Id);
            product.SelectedLengthList = LengthDal.GetAllLengthByProductIdForEditProduct(Id);
            if (Request.IsAuthenticated)
            {
                ViewBag.PageTittle = "Edit Product";
                if (type == "cat")
                {
                    ViewBag.RequestType = "cat";
                    name = CategoryDal.GetById(product.CatId).name;
                    ViewBag.breadCrum = "<a href='/Admin/Category/index'>Category</a> >> <a href='/Admin/Product/IndexFromCategory/" + product.CatId + "'>" + name + "</a> >> Edit "+product.Name;
                    product.CatId = product.CatId;
                }
                else
                {
                    ViewBag.RequestType = "brand";
                    //name = BrandDal.GetById(Id).BrandName;
                    ViewBag.breadCrum = "<a href='/Admin/Brand/index'>Brand</a> >> <a href='/Admin/Product/IndexFromBrand/" + product.BrandId + "'>" + name + "</a>  >> Edit "+product.Name;
                    product.BrandId = product.BrandId;
                }
                return View("Create",product);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(ProductModel obj)
        {
            bool check = true;
            var type="";
            if (obj.Image == null)
            {
                obj.Image = "";
            }
           var ColorlistObj= ColorDal.GetAllColor().Where(m => m.id != 0).ToList();
            obj.LengthList = LengthDal.GetAlllength().Where(m => m.id != 0).ToList();
            if (Request.IsAuthenticated)
            {
                string id = "0";
                if (obj.Id > 0)
                {
                    string path = "";
                    if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    {
                        
                        path = Path.Combine(Server.MapPath("~/ProductImages/"), obj.Image);
                        try
                        {
                            System.IO.File.Delete(path);
                        }
                        catch
                        {
                            // if there is exception in deleting the file 
                        }
                        var file = Request.Files[0];
                        var _guid = Guid.NewGuid();
                        var _fileName = "";
                        var extention = Path.GetExtension(file.FileName);
                        _fileName = _guid + extention;
                        if (file != null && file.ContentLength > 0)
                        {
                            path = Path.Combine(Server.MapPath("~/ProductImages/"), _fileName);
                            file.SaveAs(path);
                            obj.Image = _fileName;
                        }
                    }
                    
                    obj.ColorList = new List<ColorModel>();
                    for (int i = 0; i < obj.LengthList.Count; i++)
                    {

                        for (int z = 0; z < ColorlistObj.Count; z++)
                        {
                            if (Request.Form["Chk-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id] != null)
                            {
                                if (Request.Form["Chk-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id].ToString().ToUpper() == "ON")
                                {
                                    obj.ColorList.Add(
                                        new ColorModel
                                        {
                                            id = Convert.ToInt32(Request.Form["colorid-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            LengthId = Convert.ToInt32(Request.Form["LengthId-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            PriceDollar = Convert.ToDecimal(Request.Form["PriceDollar-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            PriceEuro = Convert.ToDecimal(Request.Form["PriceEuro-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            PricePound = Convert.ToDecimal(Request.Form["PricePound-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            pricenigeria = Convert.ToDecimal(Request.Form["pricenigeria-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            priceangola = Convert.ToDecimal(Request.Form["priceangola-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            priceghana = Convert.ToDecimal(Request.Form["priceghana-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            OurPriceDollar = Convert.ToDecimal(Request.Form["OurPriceDollar-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            OurPriceEuro = Convert.ToDecimal(Request.Form["OurPriceEuro-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            OurPricePound = Convert.ToDecimal(Request.Form["OurPricePound-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            ourpricenigeria = Convert.ToDecimal(Request.Form["ourpricenigeria-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            ourpriceangola = Convert.ToDecimal(Request.Form["ourpriceangola-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            ourpriceghana = Convert.ToDecimal(Request.Form["ourpriceghana-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                                            }
                                        );
                                }
                            }
                        }

                    }
                    check = ProductDal.Update(obj);
                    if (check)
                    {
                        TempData["message"] = "Saved successfully";
                    }
                    else
                    {
                        TempData["message"] = "Error while saving data";
                    }
                    return RedirectToAction("Create", "Product", new { Id = obj.Id, type = type });
                }
                else
                {
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];
                        var _guid = Guid.NewGuid();
                        var _fileName = "";
                        //var fileName = Path.GetFileName(file.FileName);
                        var extention = Path.GetExtension(file.FileName);
                        _fileName = _guid + extention;
                        if (file != null && file.ContentLength > 0)
                        {
                            var path = Path.Combine(Server.MapPath("~/ProductImages/"), _fileName);
                            file.SaveAs(path);
                            obj.Image = _fileName;
                        }
                    }
                    obj.ColorList = new List<ColorModel>();
                    for (int i = 0; i < obj.LengthList.Count; i++)
                    {

                        for (int z = 0; z < ColorlistObj.Count; z++)
                        {
                            if (Request.Form["Chk-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id] != null)
                            {
                                if (Request.Form["Chk-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id].ToString().ToUpper() == "ON")
                                {
                                    obj.ColorList.Add(
                                        new ColorModel
                                        {
                                            id = Convert.ToInt32(Request.Form["colorid-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            LengthId = Convert.ToInt32(Request.Form["LengthId-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            PriceDollar = Convert.ToDecimal(Request.Form["PriceDollar-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            PriceEuro = Convert.ToDecimal(Request.Form["PriceEuro-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            PricePound = Convert.ToDecimal(Request.Form["PricePound-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            pricenigeria = Convert.ToDecimal(Request.Form["pricenigeria-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            priceangola = Convert.ToDecimal(Request.Form["priceangola-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            priceghana = Convert.ToDecimal(Request.Form["priceghana-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            OurPriceDollar = Convert.ToDecimal(Request.Form["OurPriceDollar-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            OurPriceEuro = Convert.ToDecimal(Request.Form["OurPriceEuro-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            OurPricePound = Convert.ToDecimal(Request.Form["OurPricePound-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            ourpricenigeria = Convert.ToDecimal(Request.Form["ourpricenigeria-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            ourpriceangola = Convert.ToDecimal(Request.Form["ourpriceangola-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                            ourpriceghana = Convert.ToDecimal(Request.Form["ourpriceghana-" + ColorlistObj[z].id + "-" + obj.LengthList[i].id]),
                                        }
                                        );
                                }
                            }
                        }

                    }
                    check = ProductDal.Create(obj);  
                }
                type = Convert.ToString(Request.Form["RequestType"]);
                if (check)
                {
                    TempData["message"] = "Saved successfully";
                }
                else
                {
                    TempData["message"] = "Error while saving data";
                }
                //if (type == "cat")
                //{
                //    id = obj.CatId.ToString();
                //}
                //else
                //{
                //    id = obj.BrandId.ToString();
                //}
                return RedirectToAction("Create", "Product", new { Id = obj.CatId, type = type });
                //return RedirectToAction("Create", "Product?tId="+id+"&type="+ type);//, new { Id = id, type = type }
               
             }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            bool check = true;
            check = ProductDal.Delete(Id);
            return Json(check,JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProductPrice(int Id)
        {
            bool check = true;
            check = ProductPricingDal.Delete(Id);
            return Json(check);
        }

        [HttpGet]
        public ActionResult Copy(int id)
        {
            Session["copyProduct"] = ProductDal.GetById(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
