using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess
{
    public class LengthDal
    {
        public static bool Create(LengthModel obj)
        {

            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                context.lengths.Add(new DbEntity.length { value = obj.value + " " + "inch" });
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;

        }
        public static bool Update(LengthModel obj)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var length = context.lengths.Where(m => m.id == obj.id).FirstOrDefault();
                length.value = obj.value+" "+"inch";
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }
        public static List<LengthModel> GetAlllength()
        {
            List<LengthModel> returnObj = new List<LengthModel>();
            returnObj.Add(new LengthModel { id = 0, value = "Select" });
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var cat = context.lengths.ToList();
            foreach (var x in cat)
            {
                returnObj.Add(new LengthModel { id = x.id, value = x.value });
            }
            return returnObj;
        }
        public static LengthModel GetById(int id)
        {
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var col = context.lengths.Where(m => m.id == id).FirstOrDefault();
            var length = new LengthModel();
            length.id = col.id;
            length.value = col.value;
            return length;
        }
        //public static bool Delete(int id)
        //{

        //    bool check = true;
        //    try
        //    {
        //        var context = new Ecommerce.DbEntity.ecommerceEntities();
        //        var cat = context.lengths.Where(m => m.id == id).FirstOrDefault();
        //        var productPrices = context.productpricings.Where(m => m.LengthId == id).ToList();
        //        foreach (var x in productPrices)
        //        {
        //            check = ProductPricingDal.Delete(x.id);
        //            if (check == false)
        //            {
        //                break;
        //            }
        //        }
        //        if (check)
        //        {
        //            context.lengths.Remove(cat);
        //            context.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        check = false;
        //    }
        //    return check;

        //}

        public static List<LengthModel> GetAllLengthByProductIdForEditProduct(int id)
        {
            List<LengthModel> returnObj = new List<LengthModel>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var cat = context.productpricings.Where(m => m.productid == id).ToList();
            foreach (var x in cat)
            {
                var length = new LengthModel();
                length.id = Convert.ToInt32(x.lengthId);
                length.value = GetById(Convert.ToInt32(x.lengthId)).value;
                length.IsSelected = true;

                returnObj.Add(length);
            }
            return returnObj.ToList();
        }

        public static List<LengthModel> GetAllLengthByProductId(int id)
        {
            List<LengthModel> returnObj = new List<LengthModel>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var cat = context.productpricings.Where(m => m.productid == id).ToList();
            foreach (var x in cat)
            {
                var length = new LengthModel();
                length.id = Convert.ToInt32(x.lengthId);
                length.value = GetById(Convert.ToInt32(x.lengthId)).value;
                length.IsSelected = true;

                if (returnObj.Count == 0)
                {
                    returnObj.Add(length);
                }
                else
                {
                    if (returnObj.Where(m => m.id == x.lengthId).FirstOrDefault() == null)
                    {
                        returnObj.Add(length);
                    }
                }
            }
            return returnObj.Distinct().ToList();
        }
    }
}
