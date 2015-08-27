using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess
{
    public static class OrderDetailDal
    {
        public static bool Create(OrderDetail obj)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                context.orderdetails.Add(new DbEntity.orderdetail
                {
                    ActualPriceInUserSeletedCurrency = obj.ActualPriceInUserSeletedCurrency,
                    OrderId = obj.OrderId,
                    PricePaidInConvertedCurrency = obj.PricePaidInConvertedCurrency,
                    ProductPriceId = obj.ProductPriceId,
                    Quantity = obj.Quantity,
                    ProductName = obj.ProductName
                });
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;

        }
        public static List<OrderDetail> GetByOrderId(int id, String CurrencySeletedByUser)
        {
            List<OrderDetail> _OrderDetail = new List<OrderDetail>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var orderDetail = context.orderdetails.Where(m => m.OrderId == id).ToList();
            foreach (var obj in orderDetail)
            {
                OrderDetail _orderd = new OrderDetail();
                _orderd.ActualPriceInUserSeletedCurrency = obj.ActualPriceInUserSeletedCurrency;
                _orderd.OrderId = obj.OrderId;
                _orderd.PricePaidInConvertedCurrency = obj.PricePaidInConvertedCurrency;
                _orderd.ProductPriceId = obj.ProductPriceId;
                _orderd.Quantity = obj.Quantity;
                _orderd.ProductName = obj.ProductName;
                var ProductPricing = ProductPricingDal.GetPriceByProductPriceId(obj.ProductPriceId, CurrencySeletedByUser);
                _orderd.UnitPrice = ProductPricing.Unitprice;
                _orderd.Length = ProductPricing.LengthName;
                _orderd.Color = ProductPricing.ColorName;
               
                _OrderDetail.Add(_orderd);
            }
            return _OrderDetail;

        }
    }
}
