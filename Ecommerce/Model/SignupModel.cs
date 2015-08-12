using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Ecommerce.Model
{
    public class SignupModel
    {
        public int id { get; set; }
        [Required(ErrorMessage="Required")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter a valid e-mail adress")]
        [Remote("IsUniqueEmail", "FrontBase", AdditionalFields = "id", ErrorMessage = "This email is already used.")]
        public string Email { get; set; }
        [Required(ErrorMessage="Required")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage="Required")]
        public string Password { get; set; }
        [Required(ErrorMessage="Required")]
        public string Name { get; set; }
        [Required(ErrorMessage="Required")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }
        [Required(ErrorMessage="Required")]
        public string City { get; set; }
        [Required(ErrorMessage="Required")]
        public string State { get; set; }
        [Required(ErrorMessage="Required")]
        public string PostCode { get; set; }
        public string Country { get; set; }
        [Required(ErrorMessage="Required")]
        public string DName { get; set; }
        [Required(ErrorMessage="Required")]
        public string DAddress1 { get; set; }
        public string DAddress2 { get; set; }
        [Required(ErrorMessage="Required")]
        public string DCity { get; set; }
        [Required(ErrorMessage="Required")]
        public string DState { get; set; }
        [Required(ErrorMessage="Required")]
        public string DPostCode { get; set; }
        public string DCountry { get; set; }
        public bool Isadmin { get; set; }
    }
}
