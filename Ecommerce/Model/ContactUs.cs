using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class ContactUs
    {
        [Required]
        public string Name {get;set;}
        
        public string ContactNumber {get;set;}
         [Required]
        public string Subject {get;set;}
         [Required]
        public string Message {get;set;}
         [Required]
         public string Email { get; set; }
    }
}
