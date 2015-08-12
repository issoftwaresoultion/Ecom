using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess
{
  public static  class DeliveryDaysDal
    {
      public static Ecommerce.DbEntity.deliveryday Get()
      {
          var context = new Ecommerce.DbEntity.ecommerceEntities();
          var del = context.deliverydays.FirstOrDefault();
          return del;
      }

      public static bool Update(Ecommerce.DbEntity.deliveryday obj)
      {
          try
          {
              var context = new Ecommerce.DbEntity.ecommerceEntities();
              var del = context.deliverydays.Where(m => m.id == obj.id).FirstOrDefault();
              del.days = obj.days;
              context.SaveChanges();
              return true;
          }
          catch
          {
              return false;
          }
      }


    }
}
