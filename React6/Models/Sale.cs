//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace React6.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sale
    {
        public int id { get; set; }
        public Nullable<int> productId { get; set; }
        public Nullable<int> customerId { get; set; }
        public Nullable<int> storeId { get; set; }
        public Nullable<System.DateTime> DateSold { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
