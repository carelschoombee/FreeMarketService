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
    
    public partial class CashOrder
    {
        public int OrderId { get; set; }
        public Nullable<System.DateTime> DatePlaced { get; set; }
        public int CashCustomerId { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string Status { get; set; }
    }
}
