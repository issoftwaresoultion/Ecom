using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class ReviewModel
    {
       
            public int id { get; set; }
            [Required]
            public string Message { get; set; }
         [Required]
            public int Rating { get; set; }
         [Required]
            public string Name { get; set; }
         [Required]
         [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter a valid e-mail adress")]
         public string Email { get; set; }
            public bool active { get; set; }
            public int ProductId { get; set; }
       
    }
}
