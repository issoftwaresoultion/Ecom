using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ecommerce.Model
{
    public class LoginModel
    {
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public int id { get; set; }
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter a valid e-mail adress")]
        [Required(ErrorMessage = "Email is required")]
         public string EmailForgotPassword { get; set; }

        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter a valid e-mail adress")]
        [Remote("IsUniqueEmail", "FrontBase", AdditionalFields = "id", ErrorMessage = "This email is already used.")]
        [Required(ErrorMessage = "Email is required")]
        public string RegistrationEmail { get; set; }
        public bool isadmin { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Re-enter password is required")]
        [System.ComponentModel.DataAnnotations.CompareAttribute("Password", ErrorMessage = "Password mismatch")]
        public string rePassword { get; set; }
        public bool SubscribeForNewLetter { get; set; }


    }
}
