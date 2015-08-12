using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess
{
    public static class ProductPricingDal
    {
        public static bool Create(ProductPriceModel obj)
        {

            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();


                context.productpricings.Add(
                    new DbEntity.productpricing
                    {
                        ourpriceangola = obj.ourpriceangola,
                        ourpriceDollar = obj.OurPriceDollar,
                        ourpriceeuro = obj.OurPriceEuro,
                        ourpriceghana = obj.ourpriceghana,
                        ourpricenigeria = obj.ourpricenigeria,
                        ourpricepound = obj.OurPricePound,
                        priceangola = obj.priceangola,
                        priceDollar = obj.PriceDollar,
                        priceeuro = obj.PriceEuro,
                        priceghana = obj.priceghana,
                        pricenigeria = obj.pricenigeria,
                        pricepound = obj.PricePound,
                        productid = obj.ProductId,
                        lengthId = obj.lengthId,
                        colorId = obj.colorId

                    });

                context.SaveChanges();

            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;

        }
        public static bool Update(ProductPriceModel obj)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var cat = context.productpricings.Where(m => m.id == obj.Id).FirstOrDefault();
                cat.ourpriceangola = obj.ourpriceangola;
                cat.ourpriceDollar = obj.OurPriceDollar;
                cat.ourpriceeuro = obj.OurPriceEuro;
                cat.ourpriceghana = obj.ourpriceghana;
                cat.ourpricenigeria = obj.ourpricenigeria;
                cat.ourpricepound = obj.OurPricePound;
                cat.priceangola = obj.priceangola;
                cat.priceDollar = obj.PriceDollar;
                cat.priceeuro = obj.PriceEuro;
                cat.priceghana = obj.priceghana;
                cat.pricenigeria = obj.pricenigeria;
                cat.pricepound = obj.PricePound;
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }
        public static bool Delete(int Id)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var ProductPrice = context.productpricings.Where(m => m.productid == Id).ToList();
                foreach (var proPrice in ProductPrice)
                {
                    context.productpricings.Remove(proPrice);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }
        public static bool DeleteAllForProduct(int ProductId)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var ProductPrices = context.productpricings.Where(m => m.productid == ProductId).ToList();
                foreach (var ProductPrice in ProductPrices)
                {
                    context.productpricings.Remove(ProductPrice);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }
        public static List<ProductPriceModel> GetAllByProductId(int productId)
        {
            List<ProductPriceModel> listobj = new List<ProductPriceModel>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var productPricinglist = context.productpricings.Where(m => m.productid == productId).ToList();
            foreach (var productPricing in productPricinglist)
            {
                ProductPriceModel obj = new ProductPriceModel();
                obj.ourpriceangola = Convert.ToDecimal(productPricing.ourpriceangola);
                obj.OurPriceDollar = Convert.ToDecimal(productPricing.ourpriceDollar);
                obj.OurPriceEuro = Convert.ToDecimal(productPricing.ourpriceeuro);
                obj.ourpriceghana = Convert.ToDecimal(productPricing.ourpriceghana);
                obj.ourpricenigeria = Convert.ToDecimal(productPricing.ourpricenigeria);
                obj.OurPricePound = Convert.ToDecimal(productPricing.ourpricepound);
                obj.priceangola = Convert.ToDecimal(productPricing.priceangola);
                obj.PriceDollar = Convert.ToDecimal(productPricing.priceDollar);
                obj.PriceEuro = Convert.ToDecimal(productPricing.priceeuro);
                obj.priceghana = Convert.ToDecimal(productPricing.priceghana);
                obj.pricenigeria = Convert.ToDecimal(productPricing.pricenigeria);
                obj.PricePound = Convert.ToDecimal(productPricing.pricepound);
                obj.ProductId = Convert.ToInt32(productPricing.productid);
                obj.ColorName = ColorDal.GetById(Convert.ToInt32(productPricing.colorId)).name;
                obj.LengthName = LengthDal.GetById(Convert.ToInt32(productPricing.lengthId)).value;
                obj.colorId = Convert.ToInt32(productPricing.colorId);
                obj.lengthId = Convert.ToInt32(productPricing.lengthId);

                obj.Id = Convert.ToInt32(productPricing.id);
                listobj.Add(obj);
            }

            return listobj;
        }
        public static ProductPriceModel GetPriceByProductandLengthandColor(int productId, int LengthId, int colorId, string Currency)
        {
            ProductPriceModel obj = new ProductPriceModel();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var ProductPrice = context.productpricings.Where(m => m.productid == productId && m.colorId == colorId && m.lengthId == LengthId).FirstOrDefault();
            if (ProductPrice != null)
            {
                obj.Id = ProductPrice.id;
                if (Currency == "Dollar")
                {
                    obj.Ourprice = Convert.ToDecimal(ProductPrice.ourpriceDollar);
                    obj.Actualprice = Convert.ToDecimal(ProductPrice.priceDollar);
                }
                else if (Currency == "Euro")
                {
                    obj.Ourprice = Convert.ToDecimal(ProductPrice.ourpriceeuro);
                    obj.Actualprice = Convert.ToDecimal(ProductPrice.priceeuro);
                }
                if (Currency == "Pound")
                {
                    obj.Ourprice = Convert.ToDecimal(ProductPrice.ourpriceeuro);
                    obj.Actualprice = Convert.ToDecimal(ProductPrice.pricepound);
                }
                else if (Currency == "Naira")
                {
                    obj.Ourprice = Convert.ToDecimal(ProductPrice.ourpricenigeria);
                    obj.Actualprice = Convert.ToDecimal(ProductPrice.pricenigeria);
                }
                if (Currency == "Kwanza")
                {
                    obj.Ourprice = Convert.ToDecimal(ProductPrice.priceangola);
                    obj.Actualprice = Convert.ToDecimal(ProductPrice.ourpriceangola);
                }
                else if (Currency == "Cedi")
                {
                    obj.Ourprice = Convert.ToDecimal(ProductPrice.priceghana);
                    obj.Actualprice = Convert.ToDecimal(ProductPrice.ourpriceghana);
                }
            }
            return obj;
        }
        public static ProductPriceModel GetPriceByProductPriceId(int producPriceId, string Currency)
        {
            ProductPriceModel obj = new ProductPriceModel();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var ProductPrice = context.productpricings.Where(m => m.id == producPriceId).FirstOrDefault();
            if (ProductPrice != null)
            {
                obj.Id = ProductPrice.id;
                obj.ProductId = Convert.ToInt32(ProductPrice.productid);
                obj.LengthName=LengthDal.GetById(Convert.ToInt32(ProductPrice.lengthId)).value;
                obj.ColorName = ColorDal.GetById(Convert.ToInt32(ProductPrice.colorId)).name;
                if (Currency == "Dollar")
                {
                    obj.Unitprice = Convert.ToDecimal(ProductPrice.ourpriceDollar);
                    obj.Ourprice = Convert.ToDecimal(ProductPrice.ourpriceDollar);
                    obj.Actualprice = Convert.ToDecimal(ProductPrice.priceDollar);
                }
                else if (Currency == "Euro")
                {
                    obj.Unitprice = Convert.ToDecimal(ProductPrice.ourpriceeuro);
                    obj.Ourprice = Convert.ToDecimal(ProductPrice.ourpriceeuro);
                    obj.Actualprice = Convert.ToDecimal(ProductPrice.priceeuro);
                }
                if (Currency == "Pound")
                {
                    obj.Unitprice = Convert.ToDecimal(ProductPrice.ourpriceeuro);
                    obj.Ourprice = Convert.ToDecimal(ProductPrice.ourpriceeuro);
                    obj.Actualprice = Convert.ToDecimal(ProductPrice.pricepound);
                }
                else if (Currency == "Naira")
                {
                    obj.Unitprice = Convert.ToDecimal(ProductPrice.ourpricenigeria);
                    obj.Ourprice = Convert.ToDecimal(ProductPrice.ourpricenigeria);
                    obj.Actualprice = Convert.ToDecimal(ProductPrice.pricenigeria);
                }
                if (Currency == "Kwanza")
                {
                    obj.Unitprice = Convert.ToDecimal(ProductPrice.priceangola);
                    obj.Ourprice = Convert.ToDecimal(ProductPrice.priceangola);
                    obj.Actualprice = Convert.ToDecimal(ProductPrice.ourpriceangola);
                }
                else if (Currency == "Cedi")
                {
                    obj.Unitprice = Convert.ToDecimal(ProductPrice.priceghana);
                    obj.Ourprice = Convert.ToDecimal(ProductPrice.priceghana);
                    obj.Actualprice = Convert.ToDecimal(ProductPrice.ourpriceghana);
                }
            }
            return obj;
        }
    
    }
}
