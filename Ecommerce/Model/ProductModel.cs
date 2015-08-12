using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select category")]
        public int CatId { get; set; }
        //[Range(1, int.MaxValue, ErrorMessage = "Please select brand")]
        public int BrandId { get; set; }
        [Required]
        public string Name { get; set; }
         [Required]
        public string Discription { get; set; }
        public string Image { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public List<BrandModel> brandList { get; set; }
        public List<ExtendedCategory> CategoryList { get; set; }
        public List< ProductPriceModel> ProductPrice { get; set; }
        public List<ColorModel> ColorList { get; set; }
        public List<LengthModel> LengthList { get; set; }
        public List<ColorModel> SelectedColorList { get; set; }
        public List<LengthModel> SelectedLengthList { get; set; }
        public string KeyWord { get; set; }
        public bool Ishot { get; set; }
        public string Length { get; set; }
        public string Color { get; set; }
        public ProductPriceModel Price { get; set; }
         public int CountId { get; set; }
      
    }
}
