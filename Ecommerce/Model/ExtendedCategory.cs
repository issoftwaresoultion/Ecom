using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Ecommerce.DbEntity;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model
{
    public class ExtendedCategory
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string discription { get; set; }
        public string Code { get; set; }
    }
}
