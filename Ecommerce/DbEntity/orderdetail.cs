//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ecommerce.DbEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class orderdetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductPriceId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePaidInConvertedCurrency { get; set; }
        public decimal ActualPriceInUserSeletedCurrency { get; set; }
    }
}
