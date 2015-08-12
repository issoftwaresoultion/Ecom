using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess
{
    public class ColorDal
    {
        public static bool Create(ColorModel obj)
        {

            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                context.colors.Add(new DbEntity.color { name = obj.name });
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;

        }
        public static bool Update(ColorModel obj)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var color = context.colors.Where(m => m.id == obj.id).FirstOrDefault();
                color.name = obj.name;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }
        public static List<ColorModel> GetAllColor()
        {
            List<ColorModel> returnObj = new List<ColorModel>();
            returnObj.Add(new ColorModel { id = 0, name = "Select" });
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var cat = context.colors.ToList();
            foreach (var x in cat)
            {
                returnObj.Add(new ColorModel { id = x.id, name = x.name });
            }
            return returnObj;
        }
        public static ColorModel GetById(int id)
        {
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var col = context.colors.Where(m => m.id == id).FirstOrDefault();
            var color = new ColorModel();
            color.id = col.id;
            color.name = col.name;
            return color;
        }

        public static List<ColorModel> GetAllColorsByProductIdForEditProduct(int productId)
        {
            List<ColorModel> returnObj = new List<ColorModel>();
            //returnObj.Add(new ColorModel { id = 0, name = "Select" });
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var pricising = context.productpricings.Where(m => m.productid == productId).ToList();
            foreach (var x in pricising)
            {
                var color = new ColorModel();
                color.id = Convert.ToInt32(x.colorId);
                color.IsSelected = true;
                color.LengthId = Convert.ToInt32(x.lengthId);
                color.name = GetById(Convert.ToInt32(x.colorId)).name;
                color.ourpriceangola = x.ourpriceangola;
                color.OurPriceDollar = x.ourpriceDollar;
                color.OurPriceEuro = x.ourpriceeuro;
                color.ourpriceghana = x.ourpriceghana;
                color.ourpricenigeria = x.ourpricenigeria;
                color.OurPricePound = x.ourpricepound;
                color.priceangola = x.priceangola;
                color.PriceDollar = x.priceDollar;
                color.PriceEuro = x.priceeuro;
                color.priceghana = x.priceghana;
                color.pricenigeria = x.pricenigeria;
                color.PricePound = x.pricepound;
                returnObj.Add(color);
            }
            return returnObj;
        }

        public static List<ColorModel> GetAllColorsByProductId(int productId)
        {
            List<ColorModel> returnObj = new List<ColorModel>();
            //returnObj.Add(new ColorModel { id = 0, name = "Select" });
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var pricising = context.productpricings.Where(m => m.productid == productId).ToList();
            foreach (var x in pricising)
            {
                var color = new ColorModel();
                color.id = Convert.ToInt32(x.colorId);
                color.IsSelected = true;
                color.LengthId = Convert.ToInt32(x.lengthId);
                color.name = GetById(Convert.ToInt32(x.colorId)).name;
                color.ourpriceangola = Convert.ToDecimal(x.ourpriceangola);
                color.OurPriceDollar = Convert.ToDecimal(x.ourpriceDollar);
                color.OurPriceEuro = Convert.ToDecimal(x.ourpriceeuro);
                color.ourpriceghana = Convert.ToDecimal(x.ourpriceghana);
                color.ourpricenigeria = Convert.ToDecimal(x.ourpricenigeria);
                color.OurPricePound = Convert.ToDecimal(x.ourpricepound);
                color.priceangola = Convert.ToDecimal(x.priceangola);
                color.PriceDollar = Convert.ToDecimal(x.priceDollar);
                color.PriceEuro = Convert.ToDecimal(x.priceeuro);
                color.priceghana = Convert.ToDecimal(x.priceghana);
                color.pricenigeria = Convert.ToDecimal(x.pricenigeria);
                color.PricePound = Convert.ToDecimal(x.pricepound);


                if (returnObj.Count == 0)
                {
                    returnObj.Add(color);
                }
                else
                {
                    if (returnObj.Where(m => m.id == x.colorId).FirstOrDefault() == null)
                    {
                        returnObj.Add(color);
                    }
                }
            }
            return returnObj;
        }

        public static List<ColorModel> GetColorByProductandLength(int productId,int lengthId)
        {
            List<ColorModel> returnObj = new List<ColorModel>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var productPrice = context.productpricings.Where(m=>m.productid==productId&&m.lengthId==lengthId).ToList();
            foreach (var x in productPrice)
            {
                var color = new ColorModel();
                color.id = Convert.ToInt32(x.colorId);
                color.IsSelected = true;
                color.LengthId = Convert.ToInt32(x.lengthId);
                color.name = GetById(Convert.ToInt32(x.colorId)).name;
                color.ourpriceangola = Convert.ToDecimal(x.ourpriceangola);
                color.OurPriceDollar = Convert.ToDecimal(x.ourpriceDollar);
                color.OurPriceEuro = Convert.ToDecimal(x.ourpriceeuro);
                color.ourpriceghana = Convert.ToDecimal(x.ourpriceghana);
                color.ourpricenigeria = Convert.ToDecimal(x.ourpricenigeria);
                color.OurPricePound = Convert.ToDecimal(x.ourpricepound);
                color.priceangola = Convert.ToDecimal(x.priceangola);
                color.PriceDollar = Convert.ToDecimal(x.priceDollar);
                color.PriceEuro = Convert.ToDecimal(x.priceeuro);
                color.priceghana = Convert.ToDecimal(x.priceghana);
                color.pricenigeria = Convert.ToDecimal(x.pricenigeria);
                color.PricePound = Convert.ToDecimal(x.pricepound);


                if (returnObj.Count == 0)
                {
                    returnObj.Add(color);
                }
                else
                {
                    if (returnObj.Where(m => m.id == x.colorId).FirstOrDefault() == null)
                    {
                        returnObj.Add(color);
                    }
                }
            }
            return returnObj;
        }

        //public static bool Delete(int id)
        //{

        //    bool check = true;
        //    try
        //    {
        //        var context = new Ecommerce.DbEntity.ecommerceEntities();
        //        var cat = context.colors.Where(m => m.id == id).FirstOrDefault();
        //        var productPrices = context.productpricings.Where(m => m.ColorId == id).ToList();
        //        foreach (var x in productPrices)
        //        {
        //            check = ProductPricingDal.Delete(x.id);
        //            if (check == false)
        //            {
        //                break;
        //            }
        //        }
        //        if (check)
        //        {
        //            context.categories.Remove(cat);
        //            context.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        check = false;
        //    }
        //    return check;

        //}
    }
}
