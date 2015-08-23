using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Model;

namespace Ecommerce.DataAccess
{
    public static class ProductDal
    {
        public static bool Create(ProductModel obj)
        {

            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var newProduct = new DbEntity.product();
                newProduct.brandid = obj.BrandId;
                newProduct.catid = obj.CatId;
                newProduct.discription = obj.Discription;
                newProduct.image = obj.Image;
                newProduct.name = obj.Name;
                //newProduct.productCode = +obj.Name.Replace(" ", "-");
                newProduct.keyWord = obj.KeyWord;
                newProduct.isHot = obj.Ishot;

                context.products.Add(newProduct);
                context.SaveChanges();
                foreach (var x in obj.ColorList)
                {

                    var productPricing = new ProductPriceModel();
                    productPricing.ProductId = newProduct.id;
                    productPricing.lengthId = x.LengthId;
                    productPricing.colorId = x.id;
                    productPricing.ourpriceangola = x.ourpriceangola;
                    productPricing.OurPriceDollar = x.OurPriceDollar;
                    productPricing.OurPriceEuro = x.OurPriceEuro;
                    productPricing.ourpriceghana = x.ourpriceghana;
                    productPricing.ourpricenigeria = x.ourpricenigeria;
                    productPricing.OurPricePound = x.OurPricePound;
                    productPricing.priceangola = x.priceangola;
                    productPricing.PriceDollar = x.PriceDollar;
                    productPricing.PriceEuro = x.PriceEuro;
                    productPricing.priceghana = x.priceghana;
                    productPricing.pricenigeria = x.pricenigeria;
                    productPricing.PricePound = x.PricePound;
                    check = ProductPricingDal.Create(productPricing);

                }
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;

        }
        public static bool Update(ProductModel obj)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var Product = context.products.Where(m => m.id == obj.Id).FirstOrDefault();
                Product.brandid = obj.BrandId;
                Product.catid = obj.CatId;
                Product.discription = obj.Discription;
                Product.image = obj.Image;
                Product.name = obj.Name;
                Product.keyWord = obj.KeyWord;
                Product.isHot = obj.Ishot;
                context.SaveChanges();
                ProductPricingDal.Delete(obj.Id);
                foreach (var x in obj.ColorList)
                {

                    var productPricing = new ProductPriceModel();
                    productPricing.ProductId = Product.id;
                    productPricing.lengthId = x.LengthId;
                    productPricing.colorId = x.id;
                    productPricing.ourpriceangola = x.ourpriceangola;
                    productPricing.OurPriceDollar = x.OurPriceDollar;
                    productPricing.OurPriceEuro = x.OurPriceEuro;
                    productPricing.ourpriceghana = x.ourpriceghana;
                    productPricing.ourpricenigeria = x.ourpricenigeria;
                    productPricing.OurPricePound = x.OurPricePound;
                    productPricing.priceangola = x.priceangola;
                    productPricing.PriceDollar = x.PriceDollar;
                    productPricing.PriceEuro = x.PriceEuro;
                    productPricing.priceghana = x.priceghana;
                    productPricing.pricenigeria = x.pricenigeria;
                    productPricing.PricePound = x.PricePound;
                    check = ProductPricingDal.Create(productPricing);

                }

            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }
        public static bool Delete(int ProductId)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                check = ProductPricingDal.DeleteAllForProduct(ProductId);
                if (check)
                {
                    var Product = context.products.Where(m => m.id == ProductId).FirstOrDefault();
                    context.products.Remove(Product);
                    context.SaveChanges();
                }
            }
            catch
            {
                check = false;
            }
            return check;
        }


        public static List<ProductModel> GetByProductCategory(int catId)
        {
            List<ProductModel> Obj = new List<ProductModel>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var product = context.products.Where(m => m.catid == catId).ToList();
            foreach (var x in product)
            {
                Obj.Add(new ProductModel
                {
                    //Brand = BrandDal.GetById(Convert.ToInt32(x.brandid)).BrandName,
                    Category = CategoryDal.GetById(Convert.ToInt32(x.catid)).name,
                    Discription = x.discription,
                    Image = string.IsNullOrEmpty(x.image) == false ? x.image : "NoImage.jpg",
                    Name = x.name,
                    Id = x.id,
                    ProductPrice = ProductPricingDal.GetAllByProductId(x.id),
                    SelectedColorList = ColorDal.GetAllColorsByProductId(x.id).Where(m => m.id > 0).ToList(),
                    SelectedLengthList = LengthDal.GetAllLengthByProductId(x.id).Where(m => m.id > 0).ToList()

                });
            }
            return Obj;
        }

        public static List<ProductModel> GetRelatedProducts(string keyWord, int productId)
        {
            int count = 0;
            List<ProductModel> Obj = new List<ProductModel>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var product = context.products.Where(m => m.keyWord == keyWord && m.id != productId).ToList();
            foreach (var x in product)
            {
                count = count + 1;
                Obj.Add(new ProductModel
                {

                    //Brand = BrandDal.GetById(Convert.ToInt32(x.brandid)).BrandName,
                    Category = CategoryDal.GetById(Convert.ToInt32(x.catid)).name,
                    Discription = x.discription,
                    Image = string.IsNullOrEmpty(x.image) == false ? x.image : "NoImage.jpg",
                    Name = x.name,
                    Id = x.id,
                    ProductPrice = ProductPricingDal.GetAllByProductId(x.id),
                    SelectedColorList = ColorDal.GetAllColorsByProductId(x.id).Where(m => m.id > 0).ToList(),
                    SelectedLengthList = LengthDal.GetAllLengthByProductId(x.id).Where(m => m.id > 0).ToList()

                });
                if (count == 4)
                {
                    break;
                }
            }
            return Obj;
        }

        public static ProductModel GetByProductByProductId(int ProductId)
        {
            ProductModel Obj = new ProductModel();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var product = context.products.Where(m => m.id == ProductId).FirstOrDefault();

            Obj = new ProductModel
            {
                //Brand = BrandDal.GetById(Convert.ToInt32(x.brandid)).BrandName,
                Category = CategoryDal.GetById(Convert.ToInt32(product.catid)).name,
                Discription = product.discription,
                Image = string.IsNullOrEmpty(product.image) == false ? product.image : "NoImage.jpg",
                Name = product.name,
                Id = product.id,
                ProductPrice = ProductPricingDal.GetAllByProductId(product.id),
                SelectedColorList = ColorDal.GetAllColorsByProductId(product.id).Where(m => m.id > 0).ToList(),
                SelectedLengthList = LengthDal.GetAllLengthByProductId(product.id).Where(m => m.id > 0).ToList(),
                KeyWord = product.keyWord,
                CatId = Convert.ToInt32(product.catid),

            };

            return Obj;
        }

        public static List<ProductModel> GetByProducBrand(int brandId)
        {
            List<ProductModel> Obj = new List<ProductModel>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var product = context.products.Where(m => m.brandid == brandId).ToList();
            foreach (var x in product)
            {
                Obj.Add(new ProductModel
                {
                    //Brand = BrandDal.GetById(Convert.ToInt32(x.brandid)).BrandName,
                    Category = CategoryDal.GetById(Convert.ToInt32(x.catid)).name,
                    Discription = x.discription,
                    Image = string.IsNullOrEmpty(x.image) == false ? x.image : "NoImage.jpg",
                    Name = x.name,
                    Id = x.id,
                    ProductPrice = ProductPricingDal.GetAllByProductId(x.id)

                });
            }
            return Obj;
        }

        public static ProductModel GetById(int id)
        {
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var Product = context.products.Where(m => m.id == id).FirstOrDefault();
            var Productobj = new ProductModel();
            //Productobj.Brand = BrandDal.GetById(Convert.ToInt32(Product.brandid)).BrandName;
            Productobj.Category = CategoryDal.GetById(Convert.ToInt32(Product.catid)).name;
            Productobj.Discription = Product.discription;
            Productobj.Image = string.IsNullOrEmpty(Product.image) == false ? Product.image : "NoImage.jpg";
            Productobj.Name = Product.name;
            Productobj.ProductPrice = ProductPricingDal.GetAllByProductId(Product.id);
            Productobj.Id = id;
            Productobj.CatId = Convert.ToInt32(Product.catid);
            Productobj.Ishot = Convert.ToBoolean(Product.isHot);
            Productobj.KeyWord = Product.keyWord;
            // Productobj.BrandId = Convert.ToInt32(Product.brandid);
            return Productobj;
        }

        public static List<ProductModel> GetHotProduct()
        {
            List<ProductModel> Obj = new List<ProductModel>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var product = context.products.Where(m => m.isHot == true).ToList();
            foreach (var x in product)
            {
                Obj.Add(new ProductModel
                {
                    //Brand = BrandDal.GetById(Convert.ToInt32(x.brandid)).BrandName,
                    Category = CategoryDal.GetById(Convert.ToInt32(x.catid)).name,
                    Discription = x.discription,
                    Image = x.image != null ? x.image : "NoImage.jpg",
                    Name = x.name,
                    Id = x.id,
                    ProductPrice = ProductPricingDal.GetAllByProductId(x.id),

                });
            }
            return Obj;
        }

        public static List<ProductModel> Search(string keyword)
        {
            List<ProductModel> Obj = new List<ProductModel>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var product = context.products.Where(m => m.name.Contains(keyword)).ToList();
            foreach (var x in product)
            {
                Obj.Add(new ProductModel
                {
                    //Brand = BrandDal.GetById(Convert.ToInt32(x.brandid)).BrandName,
                    Category = CategoryDal.GetById(Convert.ToInt32(x.catid)).name,
                    Discription = x.discription,
                    Image = x.image != null ? x.image : "NoImage.jpg",
                    Name = x.name,
                    Id = x.id,
                    ProductPrice = ProductPricingDal.GetAllByProductId(x.id),

                });
            }
            return Obj;
        }

        public static void EmptyDataBase()
        {
            List<ProductModel> Obj = new List<ProductModel>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var products = context.products.ToList();
            foreach (var x in products)
            {
                context.products.Remove(x);
            }
            context.SaveChanges();

            //var productPrices = context.productpricings.ToList();
            //foreach (var x in productPrices)
            //{
            //    context.productpricings.Remove(x);
            //}
            //context.SaveChanges();

            //var OrderDetail = context.orderdetails.ToList();
            //foreach (var x in OrderDetail)
            //{
            //    context.orderdetails.Remove(x);
            //}
            //context.SaveChanges();

            var OrderHeader = context.orderheaders.ToList();
            foreach (var x in OrderHeader)
            {
                context.orderheaders.Remove(x);
            }
            context.SaveChanges();

        }
    }
}
