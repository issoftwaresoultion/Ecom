using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess
{
    public static class CategoryDal
    {
        public static bool Create(ExtendedCategory obj)
        {

            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                context.categories.Add(new DbEntity.category {Code=obj.name.Replace(" ","-"),discription=obj.discription,name=obj.name });
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;

        }
        public static bool Update(ExtendedCategory obj)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var cat = context.categories.Where(m => m.id == obj.id).FirstOrDefault();
                cat.Code = obj.name.Replace(" ", "-");
                cat.discription = obj.discription;
                cat.name = obj.name;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }
        public static List<ExtendedCategory> GetAllCategory()
        {
            List<ExtendedCategory> returnObj = new List<ExtendedCategory>();
            returnObj.Add(new ExtendedCategory { id = 0, name ="Select" });
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var cat = context.categories.ToList();
            foreach (var x in cat)
            {
                returnObj.Add(new ExtendedCategory {Code=x.Code,discription=x.discription,id=x.id,name=x.name });
            }
            return returnObj;
        }
        public static ExtendedCategory GetById(int id)
        {
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var cat = context.categories.Where(m=>m.id==id).FirstOrDefault();
            var category =new ExtendedCategory();  
            category.Code = cat.Code;
            category.discription = cat.discription; 
            category.id = cat.id;
            category.name = cat.name; 
            return category;
        }
        public static ExtendedCategory GetByCode(string code)
        {
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var cat = context.categories.Where(m => m.Code == code).FirstOrDefault();
            var category = new ExtendedCategory();
            category.Code = cat.Code;
            category.discription = cat.discription;
            category.id = cat.id;
            category.name = cat.name;
            return category;
        }
        public static bool Delete(int id)
        {

            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var cat = context.categories.Where(m => m.id == id).FirstOrDefault();
                var products = context.products.Where(m => m.catid == id).ToList();
                foreach (var x in products)
                {
                   check= ProductDal.Delete(x.id);
                   if (check == false)
                   {
                       break;
                   }
                }
                if (check)
                {
                    context.categories.Remove(cat);
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
