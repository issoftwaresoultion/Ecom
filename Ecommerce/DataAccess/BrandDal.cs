using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess
{
    public static class BrandDal
    {
        public static bool Create(BrandModel obj)
        {

            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                context.brands.Add(new DbEntity.brand { BrandName = obj.BrandName});
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;

        }
        public static bool Update(BrandModel obj)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var Brand = context.brands.Where(m => m.id == obj.id).FirstOrDefault();
                Brand.BrandName = obj.BrandName;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }
        public static List<BrandModel> GetAllBrand()
        {
            List<BrandModel> returnObj = new List<BrandModel>();
            returnObj.Add(new BrandModel { id = 0, BrandName = "Select" });
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var Brand = context.brands.ToList();
            foreach (var x in Brand)
            {
                returnObj.Add(new BrandModel { id = x.id, BrandName = x.BrandName });
            }
            return returnObj;
        }
        //public static BrandModel GetById(int id)
        //{
        //    var context = new Ecommerce.DbEntity.ecommerceEntities();
        //    var Brand = context.brands.Where(m => m.id == id).FirstOrDefault();
        //    var Brandobj = new BrandModel();
        //    Brandobj.id = Brand.id;
        //    Brandobj.BrandName = Brand.BrandName;

        //    return Brandobj;
        //}
        public static bool Delete(int id)
        {

            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var cat = context.brands.Where(m => m.id == id).FirstOrDefault();
                var products = context.products.Where(m => m.brandid == id).ToList();
                foreach (var x in products)
                {
                    check = ProductDal.Delete(x.id);
                    if (check == false)
                    {
                        break;
                    }
                }
                if (check)
                {
                    context.brands.Remove(cat);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;

        }
        
    }
}
