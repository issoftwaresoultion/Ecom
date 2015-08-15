using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductPriceId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePaidInConvertedCurrency { get; set; }
        public decimal ActualPriceInUserSeletedCurrency { get; set; }
        public string ProductName { get; set; }

    }
}
