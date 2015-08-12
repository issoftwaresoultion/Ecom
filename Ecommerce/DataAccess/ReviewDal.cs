using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess
{
    public static class ReviewDal
    {
        public static bool Create(ReviewModel obj)
        {

            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                context.reviews.Add(new DbEntity.review {
                active=false,
                email=obj.Email,
                message=obj.Message,
                name=obj.Name,
                rating=obj.Rating,
                productid=obj.ProductId,
                });
                context.SaveChanges();
            }

            catch (Exception ex)
            {
                check = false;
            }
            return check;

        }

        public static bool Update(ReviewModel obj)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var Review = context.reviews.Where(m => m.id == obj.id).FirstOrDefault();
                Review.active=obj.active;
                Review.email=obj.Email;
                Review.message=obj.Message;
                Review.name=obj.Name;
                Review.rating = obj.Rating;
                Review.productid = obj.ProductId;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }

        public static List<ReviewModel> GetAllbyProductId(int id)
        {
            List<ReviewModel> returnObj = new List<ReviewModel>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var review = context.reviews.Where(m=>m.productid==id).ToList();
            foreach (var obj in review)
            {
                returnObj.Add(new ReviewModel { id = obj.id, 
                Email=obj.email,
                Message=obj.message,
                Name=obj.name,
                Rating = Convert.ToInt32(obj.rating),
                ProductId = Convert.ToInt32(obj.productid),
                });
            }
            return returnObj;
        }
        public static List<ReviewModel> GetActiveReviewsbyProductId(int id)
        {
            List<ReviewModel> returnObj = new List<ReviewModel>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var review = context.reviews.Where(m => m.productid == id&&m.active==true).ToList();
            foreach (var obj in review)
            {
                returnObj.Add(new ReviewModel
                {
                    id = obj.id,
                    Email = obj.email,
                    Message = obj.message,
                    Name = obj.name,
                    Rating = Convert.ToInt32(obj.rating),
                    ProductId = Convert.ToInt32(obj.productid),
                });
            }
            return returnObj;
        }

        public static ReviewModel GetById(int id)
        {
            ReviewModel returnObj = null;
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var obj = context.reviews.Where(m => m.id == id).FirstOrDefault();
            returnObj = new ReviewModel
             {
                 id = obj.id,
                 Email = obj.email,
                 Message = obj.message,
                 Name = obj.name,
                 Rating = Convert.ToInt32(obj.rating),
                 ProductId = Convert.ToInt32(obj.productid),
             };
            
            return returnObj;
        }
    }
}
