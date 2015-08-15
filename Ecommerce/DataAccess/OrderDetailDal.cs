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
                context.orderdetails.Add(new DbEntity.orderdetail {
                    ActualPriceInUserSeletedCurrency=obj.ActualPriceInUserSeletedCurrency,
                    OrderId=obj.OrderId,
                    PricePaidInConvertedCurrency=obj.PricePaidInConvertedCurrency,
                    ProductPriceId=obj.ProductPriceId,
                    Quantity=obj.Quantity,
                    ProductName=obj.ProductName
                });
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        
        }
        public static List<OrderDetail> GetByOrderId(int id)
        {
            List<OrderDetail> _OrderDetail = new List<OrderDetail>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var orderDetail = context.orderdetails.Where(m => m.OrderId == id).ToList();
            foreach (var obj in orderDetail)
            {
                _OrderDetail.Add(new OrderDetail
                {
                    ActualPriceInUserSeletedCurrency = obj.ActualPriceInUserSeletedCurrency,
                    OrderId = obj.OrderId,
                    PricePaidInConvertedCurrency = obj.PricePaidInConvertedCurrency,
                    ProductPriceId = obj.ProductPriceId,
                    Quantity = obj.Quantity,
                    ProductName=obj.ProductName
                });
            }
            return _OrderDetail;

        }
    }
}
