using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess
{
    public static class UserDal
    {
        public static bool Create(SignupModel obj)
        {

            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                context.users.Add(new DbEntity.user
                {
                    Address1 = obj.Address1,
                    Address2 = obj.Address2,
                    City = obj.City,
                    ContactNumber = obj.ContactNumber,
                    Country = obj.Country,
                    DAddress1 = obj.DAddress1,
                    DAddress2 = obj.DAddress2,
                    DCity = obj.DCity,
                    DCountry = obj.DCountry,
                    DName = obj.DName,
                    DPostCode = obj.DPostCode,
                    DState = obj.DState,
                    Email = obj.Email,
                    Name = obj.Name,
                    Password = obj.Password,
                    PostCode = obj.PostCode,
                    State = obj.State,
                    Isadmin=obj.Isadmin
                });
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;

        }
        public static bool Update(SignupModel obj)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var User = context.users.Where(m => m.id == obj.id).FirstOrDefault();
                User.Address1 = obj.Address1;
                User.Address2 = obj.Address2;
                User.City = obj.City;
                User.ContactNumber = obj.ContactNumber;
                User.Country = obj.Country;
                User.DAddress1 = obj.DAddress1;
                User.DAddress2 = obj.DAddress2;
                User.DCity = obj.DCity;
                User.DCountry = obj.DCountry;
                User.DName = obj.DName;
                User.DPostCode = obj.DPostCode;
                User.DState = obj.DState;
                User.Email = obj.Email;
                User.Name = obj.Name;
                User.Password = obj.Password;
                User.PostCode = obj.PostCode;
                User.State = obj.State;
                User.Isadmin = obj.Isadmin;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }
        public static List<SignupModel> GetAll()
        {
            List<SignupModel> returnObj = new List<SignupModel>();
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var UserList = context.users.ToList().OrderByDescending(m => m.id);
            foreach (var obj in UserList)
            {
                returnObj.Add(new SignupModel {
                    Address1 = obj.Address1,
                    Address2 = obj.Address2,
                    City = obj.City,
                    ContactNumber = obj.ContactNumber,
                    Country = obj.Country,
                    DAddress1 = obj.DAddress1,
                    DAddress2 = obj.DAddress2,
                    DCity = obj.DCity,
                    DCountry = obj.DCountry,
                    DName = obj.DName,
                    DPostCode = obj.DPostCode,
                    DState = obj.DState,
                    Email = obj.Email,
                    Name = obj.Name,
                    Password = obj.Password,
                    PostCode = obj.PostCode,
                    State = obj.State,
                    id=obj.id,
                    Isadmin=Convert.ToBoolean(obj.Isadmin)
                });
            }
            return returnObj;
        }
        public static SignupModel GetById(int id)
        {
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var obj = context.users.Where(m => m.id == id).FirstOrDefault();
            var User = new SignupModel();
            User.Address1 = obj.Address1;
            User.Address2 = obj.Address2;
            User.City = obj.City;
            User.ContactNumber = obj.ContactNumber;
            User.Country = obj.Country;
            User.DAddress1 = obj.DAddress1;
            User.DAddress2 = obj.DAddress2;
            User.DCity = obj.DCity;
            User.DCountry = obj.DCountry;
            User.DName = obj.DName;
            User.DPostCode = obj.DPostCode;
            User.DState = obj.DState;
            User.Email = obj.Email;
            User.Name = obj.Name;
            User.Password = obj.Password;
            User.PostCode = obj.PostCode;
            User.State = obj.State;
            User.id = obj.id;
            User.Isadmin = Convert.ToBoolean(obj.Isadmin);
            return User;
        }
        public static SignupModel ValidateUser(LoginModel loginobj)
        {
            SignupModel User = null;
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var obj = context.users.Where(m => m.Email == loginobj.Email && m.Password == loginobj.Password&&m.Isadmin==loginobj.isadmin).FirstOrDefault();
            if (obj != null)
            {
                User = new SignupModel();
                User.Address1 = obj.Address1;
                User.Address2 = obj.Address2;
                User.City = obj.City;
                User.ContactNumber = obj.ContactNumber;
                User.Country = obj.Country;
                User.DAddress1 = obj.DAddress1;
                User.DAddress2 = obj.DAddress2;
                User.DCity = obj.DCity;
                User.DCountry = obj.DCountry;
                User.DName = obj.DName;
                User.DPostCode = obj.DPostCode;
                User.DState = obj.DState;
                User.Email = obj.Email;
                User.Name = obj.Name;
                User.Password = obj.Password;
                User.PostCode = obj.PostCode;
                User.State = obj.State;
                User.id = obj.id;
                
            }
            return User;
        }
        public static bool Delete(int id)
        {

            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                var cat = context.users.Where(m => m.id == id).FirstOrDefault();
                context.users.Remove(cat);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;

        }
        public static bool IsUnique(string email, int userId)
        {
            bool check = false; ;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                if (userId > 0)
                {
                    var cat = context.users.Where(m => m.Email == email && m.id != userId).Count();
                    if (cat > 0)
                    {
                        check = false;
                    }
                    else
                    {
                        check = true;
                    }
                }
                else
                {
                    var cat = context.users.Where(m => m.Email == email).Count();
                    if (cat > 0)
                    {
                        check = false;
                    }
                    else
                    {
                        check = true;
                    }
                }
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }
        public static SignupModel GetByemail(string Email)
        {
            var context = new Ecommerce.DbEntity.ecommerceEntities();
            var obj = context.users.Where(m => m.Email == Email).FirstOrDefault();
            var User = new SignupModel();
            if (obj != null)
            {
                User.Address1 = obj.Address1;
                User.Address2 = obj.Address2;
                User.City = obj.City;
                User.ContactNumber = obj.ContactNumber;
                User.Country = obj.Country;
                User.DAddress1 = obj.DAddress1;
                User.DAddress2 = obj.DAddress2;
                User.DCity = obj.DCity;
                User.DCountry = obj.DCountry;
                User.DName = obj.DName;
                User.DPostCode = obj.DPostCode;
                User.DState = obj.DState;
                User.Email = obj.Email;
                User.Name = obj.Name;
                User.Password = obj.Password;
                User.PostCode = obj.PostCode;
                User.State = obj.State;
                User.id = obj.id;
                User.Isadmin = Convert.ToBoolean(obj.Isadmin);
            }
            return User;
        }
        public static bool RegisterUser(LoginModel loginobj)
        {
            bool check = true;
            try
            {
                var context = new Ecommerce.DbEntity.ecommerceEntities();
                context.users.Add(new DbEntity.user
               {
                 Name=loginobj.FirstName+" "+loginobj.LastName,
                 Password = loginobj.Password,
                 Email = loginobj.RegistrationEmail,

                 Address1 = "",
                    Address2 ="",
                    City = "",
                    ContactNumber = "",
                    Country = "",
                    DAddress1 = "",
                    DAddress2 = "",
                    DCity ="",
                    DCountry = "",
                    DName = "",
                    DPostCode = "",
                    DState = "",
                   
                    PostCode = "",
                    State = "",
                    Isadmin=false
               });
                if (loginobj.SubscribeForNewLetter)
                {
                    context.newletters.Add(new DbEntity.newletter {
                        email = loginobj.RegistrationEmail
                    });
                }
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
