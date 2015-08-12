using Ecommerce.DataAccess;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class ImageController : Controller
    {
        //
        // GET: /Admin/Image/

        public ActionResult Index()
        {
            var images = ImageDal.GetAll();
            return View(images);
        }
        
        public ActionResult Edit(int id)
        {
             if (Request.IsAuthenticated)
            {
            var images = ImageDal.GetById(id);
            ViewBag.imageSize = "";
            if (images.Type == "Bannar2" || images.Type == "Bannar1" || images.Type == "Bannar3")
            {
                ViewBag.imageSize = "Image size should be 1200 pixal in width 350 pixal in height";
            }
            else if (images.Type == "Logo")
            {
                ViewBag.imageSize = "Image size should be 131 pixal in width 101 pixal in height";
            }
            else if (images.Type == "")
            {

            }
            return View(images);
            }
             else
             {
                 return RedirectToAction("index", "home");
             }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(ImagesModel obj)
        {
          bool check = true;
          if (Request.IsAuthenticated)
          {
              string path = "";
              if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
              {

                  path = Path.Combine(Server.MapPath("~/img/"), obj.image);
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
                  
                  _fileName = obj.Type  + extention;
                  if (file != null && file.ContentLength > 0)
                  {
                      path = Path.Combine(Server.MapPath("~/img/"), _fileName);
                      file.SaveAs(path);
                      obj.image = _fileName;
                  }
              }
              check = ImageDal.Update(obj);
              if (check)
              {
                  TempData["message"] = "Saved successfully";
              }
              else
              {
                  TempData["message"] = "Error while saving data";
              }
              return RedirectToAction("index", "image");
          }
          else
          {
              return RedirectToAction("index", "home");
          }
        }

    }
}
