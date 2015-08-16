using Ecommerce.Model;
using Ecommerce.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess
{
    public static class CartDal
    {
        public static CartModel AddItemTocart(int productPriceId, CartModel obj, string Currency, int? quantity)
        {
            bool check = true;
            decimal total = 0;
            decimal MinFreeDeliverammount = 0;
            if (obj == null)
            {
                obj = new CartModel();
                obj.Product = new List<ProductModel>();
            }
            if (obj.Product == null)
            {
                obj.Product = new List<ProductModel>();
            }

            if (quantity.HasValue)
            {
                for (int i = 0; i < obj.Product.Count; i++)
                {
                    if (obj.Product[i].Price.Id == productPriceId)
                    {
                        obj.Product[i].Price.Quantity = Convert.ToInt32(quantity);
                        check = false;
                    }
                }
            }

            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var productPrice = new ProductPriceModel();
            productPrice = ProductPricingDal.GetPriceByProductPriceId(productPriceId, Currency);

            var product = new ProductModel();
            product = ProductDal.GetById(productPrice.ProductId);
            product.Price = productPrice;
            if (check)
            {
                if (quantity.HasValue)
                {
                    product.Price.Quantity = Convert.ToInt32(quantity);
                }
                obj.Product.Add(product);

            }

            var deliveryCharges = DeliveryDal.Get();
            if (Currency == "Dollar")
            {
                obj.DelivierCharges = (decimal)deliveryCharges.deliveryDoller;
                MinFreeDeliverammount = (decimal)deliveryCharges.freeDeliveryAmountDoller;
            }
            else if (Currency == "Euro")
            {
                obj.DelivierCharges = (decimal)deliveryCharges.deliveryEuro;
                MinFreeDeliverammount = (decimal)deliveryCharges.freeDeliveryAmountEuro;
            }
            else if (Currency == "Pound")
            {
                obj.DelivierCharges = (decimal)deliveryCharges.deliveryPound;
                MinFreeDeliverammount = (decimal)deliveryCharges.freeDeliveryAmountPound;
            }
            else if (Currency == "Naira")
            {
                obj.DelivierCharges = (decimal)deliveryCharges.deliveryNigria;
                MinFreeDeliverammount = (decimal)deliveryCharges.freeDeliveryAmountNigria;
            }
            else if (Currency == "Kwanza")
            {
                obj.DelivierCharges = (decimal)deliveryCharges.deliveryAngola;
                MinFreeDeliverammount = (decimal)deliveryCharges.freeDeliveryAmountAngola;
            }
            else if (Currency == "Cedi")
            {
                obj.DelivierCharges = (decimal)deliveryCharges.deliveryGhana;
                MinFreeDeliverammount = (decimal)deliveryCharges.freeDeliveryAmountGhana;
            }



            for (int i = 0; i < obj.Product.Count; i++)
            {
                decimal _total = Convert.ToDecimal(obj.Product[i].Price.Ourprice) * obj.Product[i].Price.Quantity;
                obj.Product[i].Price.TotalPrice = obj.Product[i].Price.Ourprice * obj.Product[i].Price.Quantity;
                total = total + _total;
                obj.Product[i].CountId = i;
            }
            obj.Difference = MinFreeDeliverammount - total;
            if (obj.Difference <= 0)
            {
                obj.Difference = 0;
                obj.DelivierCharges = 0;
            }

            obj.SubTotalTotal = total;
            obj.Total = total + obj.DelivierCharges;


            var validate = CoupanDal.ValidateCoupan(Convert.ToString(obj.DiscountCoupan));
            if (validate != null)
            {
                obj.DiscountCoupan = obj.DiscountCoupan;
                obj.DiscountAmmount = (validate.DiscountPercentage / obj.SubTotalTotal) * 100;
                obj.Total = obj.Total - obj.DiscountAmmount;
                obj.Total = Math.Round(obj.Total, 2);
                obj.DiscountAmmount = Math.Round(obj.DiscountAmmount, 2);
            }
            return obj;
        }

        public static CartModel UpdateCurrencyChange(CartModel obj, string Currency)
        {
            List<ReCalculateCart> qtyList = new List<ReCalculateCart>();
            if (obj == null)
            {
                obj = new CartModel();
                obj.Product = new List<ProductModel>();
            }
            obj = new CartModel();
            if (obj.Product != null)
            {
                foreach (var x in obj.Product)
                {
                    qtyList.Add(new ReCalculateCart { Id = x.Price.Id, Quantity = x.Price.Quantity });
                }
           }
            obj = new CartModel();
            foreach (var x in qtyList)
            {
                AddItemTocart(x.Id, obj, Currency, x.Quantity);
            }
            return obj;
        }

        public static CartModel Delete(CartModel obj, string Currency, int rowid)
        {
            List<ReCalculateCart> qtyList = new List<ReCalculateCart>();
            if (obj == null)
            {
                obj = new CartModel();
                obj.Product = new List<ProductModel>();
            }
            foreach (var x in obj.Product)
            {
                if (!(x.CountId == rowid))
                {
                    qtyList.Add(new ReCalculateCart { Id = x.Price.Id, Quantity = x.Price.Quantity });
                }
            }
            obj = new CartModel();
            foreach (var x in qtyList)
            {
                AddItemTocart(x.Id, obj, Currency, x.Quantity);
            }
            return obj;
        }

        public static CartModel UpdateQuantity(CartModel obj, string Currency, int rowid, int quantity)
        {
            List<ReCalculateCart> qtyList = new List<ReCalculateCart>();
            if (obj == null)
            {
                obj = new CartModel();
                obj.Product = new List<ProductModel>();
            }
            foreach (var x in obj.Product)
            {
                if (!(x.CountId == rowid))
                {
                    qtyList.Add(new ReCalculateCart { Id = x.Price.Id, Quantity = x.Price.Quantity });
                }
                else
                {
                    qtyList.Add(new ReCalculateCart { Id = x.Price.Id, Quantity = quantity });
                }
            }
            obj = new CartModel();
            foreach (var x in qtyList)
            {
                AddItemTocart(x.Id, obj, Currency, x.Quantity);
            }
            return obj;
        }

        public static int SaveOrUpdateCartAsOrder(CartModel obj, string Currency, int userid, int orderId)
        {
            OrderHeader Orderheaderobj = new OrderHeader();
            Orderheaderobj.OrderDetail = new List<OrderDetail>();
            if ((Currency != "Dollar") && (Currency != "Euro") && (Currency != "Pound"))
            {
                Orderheaderobj.CurrencyChoosenByUser = Currency;
                Orderheaderobj.CurrencyInWhichAmmountPaid = "Dollar";
                Orderheaderobj.ActualAmountPaid = Utility.GetConvertedPrice(obj.Total, Currency, "USD");
                Orderheaderobj.AmountInCurrencyChoosenByuser = obj.Total;
                Orderheaderobj.DeliveryCharges = Utility.GetConvertedPrice(obj.DelivierCharges, Currency, "USD");
                foreach (var x in obj.Product)
                {
                    Orderheaderobj.OrderDetail.Add(new OrderDetail
                    {
                        ActualPriceInUserSeletedCurrency = x.Price.TotalPrice,
                        PricePaidInConvertedCurrency = Utility.GetConvertedPrice(x.Price.TotalPrice, Currency, "USD"),
                        ProductPriceId = x.Price.Id,
                        Quantity = x.Price.Quantity,
                        //ProductName = x.Name + " | Length: " + x.Price.LengthName + " inch | Color: " + x.Price.ColorName,
                        ProductName = x.Name
                    });
                }
            }
            else
            {
                Orderheaderobj.CurrencyChoosenByUser = Currency;
                Orderheaderobj.CurrencyInWhichAmmountPaid = Currency;
                Orderheaderobj.ActualAmountPaid = obj.Total;
                Orderheaderobj.AmountInCurrencyChoosenByuser = obj.Total;
                Orderheaderobj.DeliveryCharges = obj.DelivierCharges;
                foreach (var x in obj.Product)
                {
                    Orderheaderobj.OrderDetail.Add(new OrderDetail
                    {
                        ActualPriceInUserSeletedCurrency = x.Price.TotalPrice,
                        PricePaidInConvertedCurrency = x.Price.TotalPrice,
                        ProductPriceId = x.Price.Id,
                        Quantity = x.Price.Quantity,
                        ProductName = x.Name,
                    });
                }
            }
            Orderheaderobj.CreatedDate = DateTime.Now.ToShortDateString();
            Orderheaderobj.OrderStatus = "Pending";
            Orderheaderobj.PaymentStatus = "Not Recived";
            Orderheaderobj.PermotionCode = obj.DiscountCoupan;
            Orderheaderobj.Userid = userid;
            if (orderId > 0)
            {
                Orderheaderobj.orderID = orderId;
                return OrderDal.Update(Orderheaderobj);
            }
            else
            {
                return OrderDal.Create(Orderheaderobj);
            }
        }



    }
}
