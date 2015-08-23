using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class CartModel
    {
        int id { get; set; }
        public List<ProductModel> Product { get; set; }
        public string DelivierDays { get; set; }
        public int DelivierId { get; set; }
        public decimal DelivierCharges { get; set; }
        public decimal Total { get; set; }
        public decimal SubTotalTotal { get; set; }
        public decimal Difference { get; set; }
        public decimal AllProductPrice { get; set; }
        public string DiscountCoupan { get; set; }
        public decimal DiscountAmmount { get; set; }
    }
}
