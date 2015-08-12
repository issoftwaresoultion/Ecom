using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class LengthModel
    {
        public int id { get; set; }
        [Required]
        public string value { get; set; }
        public bool IsSelected { get; set; }
    }
}
