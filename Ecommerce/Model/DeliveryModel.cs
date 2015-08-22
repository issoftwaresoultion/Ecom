using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class DeliveryModel
    {
        public int id { get; set; }
        public Nullable<decimal> deliveryDoller { get; set; }
        public Nullable<decimal> deliveryPound { get; set; }
        public Nullable<decimal> deliveryEuro { get; set; }
        public Nullable<decimal> deliveryNigria { get; set; }
        public Nullable<decimal> deliveryAngola { get; set; }
        public Nullable<decimal> deliveryGhana { get; set; }
        public Nullable<decimal> freeDeliveryAmountDoller { get; set; }
        public Nullable<decimal> freeDeliveryAmountPound { get; set; }
        public Nullable<decimal> freeDeliveryAmountEuro { get; set; }
        public Nullable<decimal> freeDeliveryAmountNigria { get; set; }
        public Nullable<decimal> freeDeliveryAmountAngola { get; set; }
        public Nullable<decimal> freeDeliveryAmountGhana { get; set; }
        public string DeliveryDays { get; set; }
        public bool Default { get; set; }
        public bool Active { get; set; }
    }
}
