using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
   public class BrandModel
    {
       public int id { get; set; }
       [Required]
       public string BrandName { get; set; }
    }
}
