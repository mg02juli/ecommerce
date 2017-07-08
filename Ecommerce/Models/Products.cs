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
    using System.ComponentModel.DataAnnotations;
    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            this.Order_Items = new HashSet<Order_Items>();
            this.Product_Image = new HashSet<Product_Image>();
        }
    
        public int product_id { get; set; }
        public byte product_type_code { get; set; }
        public string product_name { get; set; }
        // [DisplayFormat(DataFormatString = "{0:C0}")]
        //[DataType(DataType.Currency)]
        public decimal product_price { get; set; }
        public string product_size { get; set; }
        public string product_color { get; set; }
        public string special_offer { get; set; }
        public string product_description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Items> Order_Items { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_Image> Product_Image { get; set; }
        public virtual Ref_Product_Type Ref_Product_Type { get; set; }
    }
}
