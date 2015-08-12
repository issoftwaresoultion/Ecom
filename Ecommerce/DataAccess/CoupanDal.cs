using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess
{
    public static class CoupanDal
    {
        public static bool Create(CoupanModel obj)
        {

            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                context.coupans.Add(new DbEntity.coupan { code = obj.code, discountpercentage = ((decimal)obj.DiscountPercentage), fromDate = obj.fromDate, toDate = obj.toDate });
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;

        }
        public static bool Update(CoupanModel obj)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var Coupan = context.coupans.Where(m => m.id == obj.id).FirstOrDefault();
                Coupan.code = obj.code;
                Coupan.fromDate = Convert.ToDateTime(obj.fromDate);
                Coupan.toDate = Convert.ToDateTime(obj.toDate);
                Coupan.discountpercentage = Convert.ToDecimal(obj.DiscountPercentage);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }
        public static List<CoupanModel> GetAll()
        {
            List<CoupanModel> returnObj = new List<CoupanModel>();
           
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var cat = context.coupans.ToList().OrderByDescending(m=>m.id);
            foreach (var x in cat)
            {
                returnObj.Add(new CoupanModel { code = x.code, fromDate = Convert.ToDateTime(x.fromDate), toDate = Convert.ToDateTime(x.toDate), id = x.id, DiscountPercentage = (decimal)x.discountpercentage });
            }
            return returnObj;
        }
        public static CoupanModel GetById(int id)
        {
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var cat = context.coupans.Where(m => m.id == id).FirstOrDefault();
            var Coupan = new CoupanModel();
            Coupan.id = cat.id;
            Coupan.code = cat.code;
            Coupan.fromDate = Convert.ToDateTime(cat.fromDate);
            Coupan.toDate = Convert.ToDateTime(cat.toDate);
            Coupan.DiscountPercentage = (decimal)cat.discountpercentage;
            return Coupan;
        }
        public static CoupanModel ValidateCoupan(string code)
        {
            CoupanModel Coupan = null;
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var cat = context.coupans.Where(m => m.code == code && m.fromDate < DateTime.Now && m.toDate > DateTime.Now).FirstOrDefault();
            if (cat != null)
            {
                Coupan = new CoupanModel();
                Coupan.id = cat.id;
                Coupan.code = cat.code;
                Coupan.fromDate = Convert.ToDateTime(cat.fromDate);
                Coupan.toDate = Convert.ToDateTime(cat.toDate);
                Coupan.DiscountPercentage = (decimal)cat.discountpercentage;
            }
            return Coupan;
        }
        public static bool Delete(int id)
        {

            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var cat = context.coupans.Where(m => m.id == id).FirstOrDefault();
                context.coupans.Remove(cat);
                context.SaveChanges();
              }
            catch (Exception ex)
            {
                check = false;
            }
            return check;

        }
    }
}
