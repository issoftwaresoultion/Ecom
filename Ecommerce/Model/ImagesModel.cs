using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public  class ImagesModel
    {
        public int id { get; set; }
        public string image { get; set; }
        public string Type { get; set; }
    }
}
