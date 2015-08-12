using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model{
    public class ProductPriceModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        
        public decimal PriceDollar { get; set; }
        public decimal PriceEuro { get; set; }
        public decimal PricePound { get; set; }
        public decimal OurPriceDollar { get; set; }
        public decimal OurPriceEuro { get; set; }
        public decimal OurPricePound { get; set; }
        public decimal ourpricenigeria { get; set; }
        public decimal ourpriceangola { get; set; }
        public decimal ourpriceghana { get; set; }
        public decimal priceghana { get; set; }
        public decimal priceangola { get; set; }
        public decimal pricenigeria { get; set; }
        public int colorId { get; set; }
        public int lengthId { get; set; }
        public string ColorName { get; set; }
        public string LengthName { get; set; }
        public decimal Ourprice { get; set; }
        public decimal Actualprice { get; set; }
        public decimal Unitprice { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }

 
    }
}
