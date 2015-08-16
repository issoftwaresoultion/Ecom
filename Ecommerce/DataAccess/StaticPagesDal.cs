using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DbEntity;

namespace Ecommerce.DataAccess
{
    public static class StaticPagesDal
    {
        public static bool Create(staticpage obj)
        {

            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                    context.staticpages.Add(new DbEntity.staticpage {
                    Content=obj.Content,
                     Groupby=obj.Groupby,
                     Heading=obj.Heading,
                     
                });
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;

        }
        public static bool Update(staticpage obj)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var sp = context.staticpages.Where(m => m.id == obj.id).FirstOrDefault();
                sp.Content = obj.Content;
                sp.Groupby = obj.Groupby;
                sp.Heading = obj.Heading;
               context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }
        
        public static staticpage GetById(int id)
        {
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var obj = context.staticpages.Where(m => m.id == id).FirstOrDefault();
            var sp = new staticpage();
            sp.Content = obj.Content;
            sp.Groupby = obj.Groupby;
            sp.Heading = obj.Heading;
            sp.id = obj.id;
            return sp;
        }
        public static List<staticpage> GetBygroupId(string Group)
        {
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var obj = context.staticpages.Where(m => m.Groupby == Group).ToList();
            List<staticpage> sp = new List<staticpage> ();
            foreach (var x in obj)
            {
                sp.Add(new staticpage { 
                Content=x.Content,
                Groupby=x.Groupby,
                Heading=x.Heading,
                id=x.id
                });
            }

            return sp;
        }

        public static List<staticpage> GetAll()
        {
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var obj = context.staticpages.ToList();
            List<staticpage> sp = new List<staticpage>();
            foreach (var x in obj)
            {
                sp.Add(new staticpage
                {
                    Content = x.Content,
                    Groupby = x.Groupby,
                    Heading = x.Heading,
                    id = x.id
                });
            }

            return sp;
        }


        public static staticpage GetByHeading(String Heading)
        {
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var obj = context.staticpages.Where(m => m.Heading == Heading).FirstOrDefault();
            var sp = new staticpage();
            sp.Content = obj.Content;
            sp.Groupby = obj.Groupby;
            sp.Heading = obj.Heading;
            sp.id = obj.id;
            return sp;
        }
    }
}
