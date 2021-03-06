//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreeMarket.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductCustodian
    {
        public int ProductNumber { get; set; }
        public int CustodianNumber { get; set; }
        public int QuantityOnHand { get; set; }
        public Nullable<System.DateTime> DateLastIncreasedBySupplier { get; set; }
        public Nullable<int> AmountLastIncreasedBySupplier { get; set; }
        public int SupplierNumber { get; set; }
        public Nullable<int> StockReservedForOrders { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Custodian Custodian { get; set; }
    }
}
