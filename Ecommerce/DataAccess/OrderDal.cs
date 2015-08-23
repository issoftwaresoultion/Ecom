using Ecommerce.DbEntity;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess
{
    public static class OrderDal
    {
        public static int Create(OrderHeader obj)
        {
            int orderId = 0;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var Order = new DbEntity.orderheader();
                Order.ActualAmountPaid = obj.ActualAmountPaid;
                Order.AmountInCurrencyChoosenByuser = obj.ActualAmountPaid;
                Order.CreatedDate = obj.CreatedDate;
                Order.CurrencyChoosenByUser = obj.CurrencyChoosenByUser;
                Order.CurrencyInWhichAmmountPaid = obj.CurrencyInWhichAmmountPaid;
                Order.OrderStatus = obj.OrderStatus;
                Order.PaymentStatus = obj.PaymentStatus;
                Order.PermotionCode = obj.PermotionCode;
                Order.Userid = obj.Userid;
                Order.DeliveryCharges = obj.DeliveryCharges;

                Order.TransId = obj.TransId;
                Order.TotalProductCostInUserCurrency = obj.TotalProductCostInUserCurrency;
                Order.TotalProductCostInConvertedrCurrency = obj.TotalProductCostInConvertedrCurrency;
                Order.Discount = obj.Discount == null ? 0 : obj.Discount;
                context.orderheaders.Add(Order);
                context.SaveChanges();
                orderId = Order.orderID;
                foreach (var x in obj.OrderDetail)
                {
                    x.OrderId = orderId;
                    OrderDetailDal.Create(x);
                }
            }
            catch (Exception ex)
            {
                orderId = 0;
            }
            return orderId;

        }

        public static OrderHeader GetByOrderId(int id)
        {
            OrderHeader Order = new OrderHeader();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var obj = context.orderheaders.Where(m => m.orderID == id).FirstOrDefault();
            Order.ActualAmountPaid = obj.ActualAmountPaid;
            Order.AmountInCurrencyChoosenByuser = obj.ActualAmountPaid;
            Order.CreatedDate = obj.CreatedDate;
            Order.CurrencyChoosenByUser = obj.CurrencyChoosenByUser;
            Order.CurrencyInWhichAmmountPaid = obj.CurrencyInWhichAmmountPaid;
            Order.OrderStatus = obj.OrderStatus;
            Order.PaymentStatus = obj.PaymentStatus;
            Order.PermotionCode = obj.PermotionCode;
            Order.Userid = obj.Userid;
            Order.orderID = obj.orderID;
            Order.Name = obj.Name;
            Order.TotalProductCostInUserCurrency = obj.TotalProductCostInUserCurrency;
            Order.TotalProductCostInConvertedrCurrency = obj.TotalProductCostInConvertedrCurrency;
            Order.Discount = obj.Discount == null ? 0 : obj.Discount;
            Order.DeliveryCharges = obj.DeliveryCharges;
            Order.Address1 = obj.Address1;
            Order.Address2 = obj.Address2;
            Order.City = obj.City;
            Order.ContactNumber = obj.ContactNumber;
            Order.Country = obj.Country;
            Order.DAddress1 = obj.DAddress1;
            Order.DAddress2 = obj.DAddress2;
            Order.DCity = obj.DCity;
            Order.DCountry = obj.DCountry;
            Order.DPostCode = obj.DPostCode;
            Order.DState = obj.DState;
            Order.Email = obj.Email;
            Order.Name = obj.Name;
            Order.PostCode = obj.PostCode;
            Order.State = obj.State;

            Order.OrderDetail = new List<OrderDetail>();
            Order.OrderDetail = OrderDetailDal.GetByOrderId(Order.orderID, obj.CurrencyChoosenByUser);
            return Order;
        }

        public static bool UpdateUserAddressInOrder(OrderHeader obj)
        {
             bool check = true;
             try
             {
                 var context = new Ecommerce.DbEntity.ecommerceEntities();
                 var Order = context.orderheaders.Where(m => m.orderID == obj.orderID).FirstOrDefault();
                 Order.Address1 = obj.Address1;
                 Order.Address2 = obj.Address2;
                 Order.City = obj.City;
                 Order.ContactNumber = obj.ContactNumber;
                 Order.Country = obj.Country;
                 Order.DAddress1 = obj.DAddress1;
                 Order.DAddress2 = obj.DAddress2;
                 Order.DCity = obj.DCity;
                 Order.DCountry = obj.DCountry;
                 Order.DPostCode = obj.DPostCode;
                 Order.DState = obj.DState;
                 Order.Email = obj.Email;
                 Order.Name = obj.Name;
                 Order.PostCode = obj.PostCode;
                 Order.State = obj.State;
                 context.SaveChanges();
             }
             catch (Exception ex)
             {
                 check = false;
             }
             return check;
        }
        internal static int Update(OrderHeader obj)
        {
            int orderId = obj.orderID;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var Order = context.orderheaders.Where(m => m.orderID == obj.orderID).FirstOrDefault();
                Order.ActualAmountPaid = obj.ActualAmountPaid;
                Order.AmountInCurrencyChoosenByuser = obj.ActualAmountPaid;
                Order.CreatedDate = obj.CreatedDate;
                Order.CurrencyChoosenByUser = obj.CurrencyChoosenByUser;
                Order.CurrencyInWhichAmmountPaid = obj.CurrencyInWhichAmmountPaid;
                Order.OrderStatus = obj.OrderStatus;
                Order.PaymentStatus = obj.PaymentStatus;
                Order.PermotionCode = obj.PermotionCode;
                Order.Userid = obj.Userid;
                Order.DeliveryCharges = obj.DeliveryCharges;
                Order.TotalProductCostInUserCurrency = obj.TotalProductCostInUserCurrency;
                Order.TotalProductCostInConvertedrCurrency = obj.TotalProductCostInConvertedrCurrency;
                Order.Discount = obj.Discount == null ? 0 : obj.Discount;
                //context.orderheaders.Add(Order);
                context.SaveChanges();
                var OrderDetail = context.orderdetails.Where(m => m.OrderId == obj.orderID).ToList();
                foreach (orderdetail x in OrderDetail)
                {
                    context.orderdetails.Remove(x);
                }
                foreach (var x in obj.OrderDetail)
                {
                    x.OrderId = orderId;
                    OrderDetailDal.Create(x);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                orderId = 0;
            }
            return orderId;
        }

        public static bool UpdatePaymentStatusInOrder(int orderid,string PaymantStatus,string TransId)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var Order = context.orderheaders.Where(m => m.orderID == orderid).FirstOrDefault();
                Order.PaymentStatus = PaymantStatus;
                Order.TransId = TransId;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }

        
    }
}
