using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
   public class CoupanModel
    {
        public int id { get; set; }
        public string code { get; set; }
       [Required]
        public System.DateTime fromDate { get; set; }
        [Required]
        public System.DateTime toDate { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}
