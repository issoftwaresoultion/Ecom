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
                    freeDeliveryAmountGhana = obj.freeDeliveryAmountGhana,
                    Active=obj.Active,
                    Default=obj.Default,
                   DeliveryDays=obj.DeliveryDays,
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
                delivery.Active=obj.Active;
                    delivery.Default=obj.Default;
                    delivery.DeliveryDays = obj.DeliveryDays;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }
        public static List<DeliveryModel> GetAll()
        {
            DeliveryModel obj; 
            List<DeliveryModel> Listobj = new List<DeliveryModel>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var cat = context.deliverycharges.ToList();
            foreach (var x in cat)
            {
                obj = new DeliveryModel();
                obj.id = x.id;
                obj.deliveryAngola = x.deliveryAngola == null ? 0 : x.deliveryAngola;
                obj.deliveryDoller = x.deliveryDoller == null ? 0 : x.deliveryDoller;
                obj.deliveryEuro = x.deliveryEuro == null ? 0 : x.deliveryEuro;
                obj.deliveryGhana = x.deliveryGhana == null ? 0 : x.deliveryGhana;
                obj.deliveryNigria = x.deliveryNigria == null ? 0 : x.deliveryNigria;
                obj.deliveryPound = x.deliveryPound == null ? 0 : x.deliveryPound;
                obj.freeDeliveryAmountDoller = x.freeDeliveryAmountDoller == null ? 0 : x.freeDeliveryAmountDoller;
                obj.freeDeliveryAmountPound = x.freeDeliveryAmountPound == null ? 0 : x.freeDeliveryAmountPound;
                obj.freeDeliveryAmountEuro = x.freeDeliveryAmountEuro == null ? 0 : x.freeDeliveryAmountEuro;
                obj.freeDeliveryAmountNigria = x.freeDeliveryAmountNigria == null ? 0 : x.freeDeliveryAmountNigria;
                obj.freeDeliveryAmountAngola = x.freeDeliveryAmountAngola == null ? 0 : x.freeDeliveryAmountAngola;
                obj.freeDeliveryAmountGhana = x.freeDeliveryAmountGhana == null ? 0 : x.freeDeliveryAmountGhana;
                obj.Active = x.Active;
                obj.Default = x.Default;
                obj.DeliveryDays = x.DeliveryDays;
                Listobj.Add(obj);
            }
            return Listobj;
        }
        public static List<DeliveryModel> GetAllActive()
        {
            DeliveryModel obj;
            List<DeliveryModel> Listobj = new List<DeliveryModel>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var cat = context.deliverycharges.Where(m=>m.Active==true).ToList();
            foreach (var x in cat)
            {
                obj = new DeliveryModel();
                obj.id = x.id;
                obj.deliveryAngola = x.deliveryAngola == null ? 0 : x.deliveryAngola;
                obj.deliveryDoller = x.deliveryDoller == null ? 0 : x.deliveryDoller;
                obj.deliveryEuro = x.deliveryEuro == null ? 0 : x.deliveryEuro;
                obj.deliveryGhana = x.deliveryGhana == null ? 0 : x.deliveryGhana;
                obj.deliveryNigria = x.deliveryNigria == null ? 0 : x.deliveryNigria;
                obj.deliveryPound = x.deliveryPound == null ? 0 : x.deliveryPound;
                obj.freeDeliveryAmountDoller = x.freeDeliveryAmountDoller == null ? 0 : x.freeDeliveryAmountDoller;
                obj.freeDeliveryAmountPound = x.freeDeliveryAmountPound == null ? 0 : x.freeDeliveryAmountPound;
                obj.freeDeliveryAmountEuro = x.freeDeliveryAmountEuro == null ? 0 : x.freeDeliveryAmountEuro;
                obj.freeDeliveryAmountNigria = x.freeDeliveryAmountNigria == null ? 0 : x.freeDeliveryAmountNigria;
                obj.freeDeliveryAmountAngola = x.freeDeliveryAmountAngola == null ? 0 : x.freeDeliveryAmountAngola;
                obj.freeDeliveryAmountGhana = x.freeDeliveryAmountGhana == null ? 0 : x.freeDeliveryAmountGhana;
                obj.Active = x.Active;
                obj.Default = x.Default;
                obj.DeliveryDays = x.DeliveryDays;
                Listobj.Add(obj);
            }
            return Listobj;
        }
        public static DeliveryModel GetDefault()
        {
           
           DeliveryModel obj = new DeliveryModel();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var cat = context.deliverycharges.Where(m=>m.Default==true).FirstOrDefault();
           
                obj = new DeliveryModel();
                obj.id = cat.id;
                obj.deliveryAngola = cat.deliveryAngola == null ? 0 : cat.deliveryAngola;
                obj.deliveryDoller = cat.deliveryDoller == null ? 0 : cat.deliveryDoller;
                obj.deliveryEuro = cat.deliveryEuro == null ? 0 : cat.deliveryEuro;
                obj.deliveryGhana = cat.deliveryGhana == null ? 0 : cat.deliveryGhana;
                obj.deliveryNigria = cat.deliveryNigria == null ? 0 : cat.deliveryNigria;
                obj.deliveryPound = cat.deliveryPound == null ? 0 : cat.deliveryPound;
                obj.freeDeliveryAmountDoller = cat.freeDeliveryAmountDoller == null ? 0 : cat.freeDeliveryAmountDoller;
                obj.freeDeliveryAmountPound = cat.freeDeliveryAmountPound == null ? 0 : cat.freeDeliveryAmountPound;
                obj.freeDeliveryAmountEuro = cat.freeDeliveryAmountEuro == null ? 0 : cat.freeDeliveryAmountEuro;
                obj.freeDeliveryAmountNigria = cat.freeDeliveryAmountNigria == null ? 0 : cat.freeDeliveryAmountNigria;
                obj.freeDeliveryAmountAngola = cat.freeDeliveryAmountAngola == null ? 0 : cat.freeDeliveryAmountAngola;
                obj.freeDeliveryAmountGhana = cat.freeDeliveryAmountGhana == null ? 0 : cat.freeDeliveryAmountGhana;
                obj.Active = cat.Active;
                obj.Default = cat.Default;
                obj.DeliveryDays = cat.DeliveryDays;
           
            return obj;
        }
        public static DeliveryModel GetById(int id)
        {

            DeliveryModel obj = new DeliveryModel();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var cat = context.deliverycharges.Where(m => m.id == id).FirstOrDefault();

            obj = new DeliveryModel();
            obj.id = cat.id;
            obj.deliveryAngola = cat.deliveryAngola == null ? 0 : cat.deliveryAngola;
            obj.deliveryDoller = cat.deliveryDoller == null ? 0 : cat.deliveryDoller;
            obj.deliveryEuro = cat.deliveryEuro == null ? 0 : cat.deliveryEuro;
            obj.deliveryGhana = cat.deliveryGhana == null ? 0 : cat.deliveryGhana;
            obj.deliveryNigria = cat.deliveryNigria == null ? 0 : cat.deliveryNigria;
            obj.deliveryPound = cat.deliveryPound == null ? 0 : cat.deliveryPound;
            obj.freeDeliveryAmountDoller = cat.freeDeliveryAmountDoller == null ? 0 : cat.freeDeliveryAmountDoller;
            obj.freeDeliveryAmountPound = cat.freeDeliveryAmountPound == null ? 0 : cat.freeDeliveryAmountPound;
            obj.freeDeliveryAmountEuro = cat.freeDeliveryAmountEuro == null ? 0 : cat.freeDeliveryAmountEuro;
            obj.freeDeliveryAmountNigria = cat.freeDeliveryAmountNigria == null ? 0 : cat.freeDeliveryAmountNigria;
            obj.freeDeliveryAmountAngola = cat.freeDeliveryAmountAngola == null ? 0 : cat.freeDeliveryAmountAngola;
            obj.freeDeliveryAmountGhana = cat.freeDeliveryAmountGhana == null ? 0 : cat.freeDeliveryAmountGhana;
            obj.Active = cat.Active;
            obj.Default = cat.Default;
            obj.DeliveryDays = cat.DeliveryDays;

            return obj;
        }
    }
}
