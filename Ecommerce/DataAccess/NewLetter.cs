using Ecommerce.DbEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess
{
   public static class NewLetter
    {
       public static bool Create(string email)
       {
           bool check = true;
           try
           {
               var context = new Ecommerce.DbEntity.ecommerceEntities();
               context.newletters.Add(new DbEntity.newletter { email = email });
               context.SaveChanges();
           }
           catch (Exception ex)
           {
               check = false;
           }
           return check;

       }

       public static List<newletter> GetAllEmail()
       {
           var context = new Ecommerce.DbEntity.ecommerceEntities();
           var NewsLetter = context.newletters.ToList();
           return NewsLetter;
       }
    }
}
