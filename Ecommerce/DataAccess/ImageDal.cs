using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess
{
    public static  class ImageDal
    {
        public static bool Update(ImagesModel obj)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var image = context.images.Where(m => m.id == obj.id).FirstOrDefault();
                image.image1 = obj.image;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }

        public static List<ImagesModel> GetAll()
        {
            List<ImagesModel> ListImage=new List<ImagesModel>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var img = context.images.ToList();
            foreach (var x in img)
            {
                ListImage.Add(new ImagesModel {id=x.id,image=x.image1,Type=x.imageType });   
            }
            return ListImage;
        }
        public static ImagesModel GetById(int id)
        {
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var col = context.images.Where(m => m.id == id).FirstOrDefault();
            var Image = new ImagesModel();
            Image.id = col.id;
            Image.image = col.image1;
            Image.Type = col.imageType;
            return Image;
        }
    }
}
