using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class ColorModel
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public bool IsSelected { get; set; }
        public int LengthId { get; set; }
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
    }
}
