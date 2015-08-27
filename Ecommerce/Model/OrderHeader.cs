using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class OrderHeader
    {
        public int orderID { get; set; }
        public int Userid { get; set; }
        public string PermotionCode { get; set; }
        public string CurrencyChoosenByUser { get; set; }
        public decimal AmountInCurrencyChoosenByuser { get; set; }
        public string CurrencyInWhichAmmountPaid { get; set; }
        public decimal ActualAmountPaid { get; set; }
        public string PaymentStatus { get; set; }
        public string OrderStatus { get; set; }
        public string CreatedDate { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public decimal DeliveryCharges { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string DAddress1 { get; set; }
        public string DAddress2 { get; set; }
        public string DCity { get; set; }
        public string DState { get; set; }
        public string DPostCode { get; set; }
        public string DCountry { get; set; }
        public string TransId { get; set; }
        public decimal TotalProductCostInUserCurrency { get; set; }
        public decimal TotalProductCostInConvertedrCurrency { get; set; }
        public decimal Discount { get; set; }
        
    }
}
