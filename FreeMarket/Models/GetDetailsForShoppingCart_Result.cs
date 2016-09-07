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
    
    public partial class GetDetailsForShoppingCart_Result
    {
        public int OrderNumber { get; set; }
        public Nullable<System.DateTime> OrderDatePlaced { get; set; }
        public Nullable<System.DateTime> OrderDateClosed { get; set; }
        public string CustomerNumber { get; set; }
        public decimal TotalOrderValue { get; set; }
        public string OrderStatus { get; set; }
        public string CustomerOverallSatisfactionRating { get; set; }
        public bool PaymentReceived { get; set; }
        public int SupplierNumberID { get; set; }
        public string SupplierName { get; set; }
        public int CourierNumberID { get; set; }
        public string CourierName { get; set; }
        public int ProductNumberID { get; set; }
        public System.DateTime DateAdded { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string Description { get; set; }
        public int DepartmentNumber { get; set; }
        public decimal Weight { get; set; }
        public string Size { get; set; }
        public bool Activated { get; set; }
        public int DepartmentNumberID { get; set; }
        public string DepartmentName { get; set; }
        public int ItemNumber { get; set; }
        public int OrderDetailOrderNumber { get; set; }
        public int SupplierNumber { get; set; }
        public int OrderDetailProductNumber { get; set; }
        public Nullable<int> CourierNumber { get; set; }
        public Nullable<bool> Settled { get; set; }
        public Nullable<bool> PaySupplier { get; set; }
        public Nullable<bool> PayCourier { get; set; }
        public Nullable<bool> PaidSupplier { get; set; }
        public Nullable<bool> PaidCourier { get; set; }
        public string OrderItemStatus { get; set; }
        public decimal PriceOrderDetail { get; set; }
        public int Quantity { get; set; }
        public decimal OrderItemValue { get; set; }
        public Nullable<System.DateTime> DeliveryDateAgreed { get; set; }
        public Nullable<System.DateTime> DeliveryDateActual { get; set; }
        public Nullable<decimal> CourierFee { get; set; }
        public string CustomerProductQualityRating { get; set; }
        public string CustomerCourierOnTimeDeliveryRating { get; set; }
        public decimal PricePerUnit { get; set; }
        public string DeliveryAddress { get; set; }
        public Nullable<int> CustodianNumber { get; set; }
    }
}
