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
    
    public partial class GetAllProductsIncludingDeactivated_Result
    {
        public int SupplierNumberID { get; set; }
        public string SupplierName { get; set; }
        public int ProductNumberID { get; set; }
        public System.DateTime DateAdded { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public int DepartmentNumber { get; set; }
        public decimal Weight { get; set; }
        public string Size { get; set; }
        public bool Activated { get; set; }
        public string DepartmentName { get; set; }
        public decimal PricePerUnit { get; set; }
        public Nullable<decimal> SpecialPricePerUnit { get; set; }
    }
}
