using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess
{
    public class DeliveryDal
    {
        public static bool Create(DeliveryModel obj)
        {

            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                context.deliverycharges.Add(new DbEntity.deliverycharge
                {
                    deliveryAngola = obj.deliveryAngola,
                    deliveryDoller = obj.deliveryDoller,
                    deliveryEuro = obj.deliveryEuro,
                    deliveryGhana = obj.deliveryGhana,
                    deliveryNigria = obj.deliveryNigria,
                    deliveryPound = obj.deliveryPound,
                    freeDeliveryAmountDoller = obj.freeDeliveryAmountDoller,
                    freeDeliveryAmountPound = obj.freeDeliveryAmountPound,
                    freeDeliveryAmountEuro = obj.freeDeliveryAmountEuro,
                    freeDeliveryAmountNigria = obj.freeDeliveryAmountNigria,
                    freeDeliveryAmountAngola = obj.freeDeliveryAmountAngola,
                    freeDeliveryAmountGhana = obj.freeDeliveryAmountGhana
                });
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;

        }
        public static bool Update(DeliveryModel obj)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var delivery = context.deliverycharges.Where(m => m.id == obj.id).FirstOrDefault();
                delivery.deliveryAngola = obj.deliveryAngola;
                delivery.deliveryDoller = obj.deliveryDoller;
                delivery.deliveryEuro = obj.deliveryEuro;
                delivery.deliveryGhana = obj.deliveryGhana;
                delivery.deliveryNigria = obj.deliveryNigria;
                delivery.deliveryPound = obj.deliveryPound;
                delivery.freeDeliveryAmountDoller = obj.freeDeliveryAmountDoller;
                delivery.freeDeliveryAmountPound = obj.freeDeliveryAmountPound;
                delivery.freeDeliveryAmountEuro = obj.freeDeliveryAmountEuro;
                delivery.freeDeliveryAmountNigria = obj.freeDeliveryAmountNigria;
                delivery.freeDeliveryAmountAngola = obj.freeDeliveryAmountAngola;
                delivery.freeDeliveryAmountGhana = obj.freeDeliveryAmountGhana;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }
        public static DeliveryModel Get()
        {
            DeliveryModel obj=new DeliveryModel();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var cat = context.deliverycharges.ToList();
            foreach (var x in cat)
            {
                    obj.id = x.id;
                    obj.deliveryAngola = x.deliveryAngola;
                    obj.deliveryDoller = x.deliveryDoller;
                    obj.deliveryEuro = x.deliveryEuro;
                    obj.deliveryGhana = x.deliveryGhana;
                    obj.deliveryNigria = x.deliveryNigria;
                    obj.deliveryPound = x.deliveryPound;
                    obj.freeDeliveryAmountDoller = x.freeDeliveryAmountDoller;
                    obj.freeDeliveryAmountPound = x.freeDeliveryAmountPound;
                    obj.freeDeliveryAmountEuro = x.freeDeliveryAmountEuro;
                    obj.freeDeliveryAmountNigria = x.freeDeliveryAmountNigria;
                    obj.freeDeliveryAmountAngola = x.freeDeliveryAmountAngola;
                    obj.freeDeliveryAmountGhana = x.freeDeliveryAmountGhana;
                
            }
            return obj;
        }

    }
}
