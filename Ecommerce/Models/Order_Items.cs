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
    
    public partial class Order_Items
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order_Items()
        {
            this.Shipments = new HashSet<Shipments>();
        }
    
        public int order_items_id { get; set; }
        public int product_id { get; set; }
        public int order_id { get; set; }
        public byte order_item_status_code { get; set; }
        public byte order_item_quantity { get; set; }
        public decimal order_item_price { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Products Products { get; set; }
        public virtual Ref_Order_Item_Status_Codes Ref_Order_Item_Status_Codes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shipments> Shipments { get; set; }
    }
}
