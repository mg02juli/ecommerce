//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product_Image
    {
        public int image_id { get; set; }
        public int product_id { get; set; }
        public string image_url { get; set; }
    
        public virtual Products Products { get; set; }
    }
}
