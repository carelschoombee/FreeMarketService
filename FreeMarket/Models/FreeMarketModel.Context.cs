﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FreeMarketEntities : DbContext
    {
        public FreeMarketEntities()
            : base("name=FreeMarketEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ActivityLogging> ActivityLoggings { get; set; }
        public virtual DbSet<AddressName> AddressNames { get; set; }
        public virtual DbSet<Courier> Couriers { get; set; }
        public virtual DbSet<CourierStockMovementLog> CourierStockMovementLogs { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<ExceptionLogging> ExceptionLoggings { get; set; }
        public virtual DbSet<FreeMarketOwner> FreeMarketOwners { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<OrderStatu> OrderStatus { get; set; }
        public virtual DbSet<PaymentGatewayMessage> PaymentGatewayMessages { get; set; }
        public virtual DbSet<PaymentGatewayParameter> PaymentGatewayParameters { get; set; }
        public virtual DbSet<PaymentGatewayPaymentMethod> PaymentGatewayPaymentMethods { get; set; }
        public virtual DbSet<PaymentGatewayTransactionStatu> PaymentGatewayTransactionStatus { get; set; }
        public virtual DbSet<PreferredCommunicationMethod> PreferredCommunicationMethods { get; set; }
        public virtual DbSet<ProductCustodian> ProductCustodians { get; set; }
        public virtual DbSet<ProductPicture> ProductPictures { get; set; }
        public virtual DbSet<SiteConfiguration> SiteConfigurations { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<SystemAction> SystemActions { get; set; }
        public virtual DbSet<AuditUser> AuditUsers { get; set; }
        public virtual DbSet<SitePicture> SitePictures { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductSupplier> ProductSuppliers { get; set; }
        public virtual DbSet<SupplierLocation> SupplierLocations { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public virtual DbSet<CourierCustodianDistance> CourierCustodianDistances { get; set; }
        public virtual DbSet<Custodian> Custodians { get; set; }
        public virtual DbSet<CourierLocation> CourierLocations { get; set; }
        public virtual DbSet<CourierReview> CourierReviews { get; set; }
        public virtual DbSet<ProductReview> ProductReviews { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; }
    
        public virtual ObjectResult<GetAllProducts_Result> GetAllProducts()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllProducts_Result>("GetAllProducts");
        }
    
        public virtual ObjectResult<GetAllProductsByDepartment_Result> GetAllProductsByDepartment(Nullable<int> departmentNumber)
        {
            var departmentNumberParameter = departmentNumber.HasValue ?
                new ObjectParameter("DepartmentNumber", departmentNumber) :
                new ObjectParameter("DepartmentNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllProductsByDepartment_Result>("GetAllProductsByDepartment", departmentNumberParameter);
        }
    
        public virtual ObjectResult<GetNumberOfItemsPerAddress_Result> GetNumberOfItemsPerAddress(Nullable<int> orderNumber)
        {
            var orderNumberParameter = orderNumber.HasValue ?
                new ObjectParameter("OrderNumber", orderNumber) :
                new ObjectParameter("OrderNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetNumberOfItemsPerAddress_Result>("GetNumberOfItemsPerAddress", orderNumberParameter);
        }
    
        public virtual ObjectResult<CalculateDeliveryFee_Result> CalculateDeliveryFee(Nullable<int> productNumber, Nullable<int> supplierNumber, Nullable<int> quantityRequested, Nullable<int> addressNumber)
        {
            var productNumberParameter = productNumber.HasValue ?
                new ObjectParameter("productNumber", productNumber) :
                new ObjectParameter("productNumber", typeof(int));
    
            var supplierNumberParameter = supplierNumber.HasValue ?
                new ObjectParameter("supplierNumber", supplierNumber) :
                new ObjectParameter("supplierNumber", typeof(int));
    
            var quantityRequestedParameter = quantityRequested.HasValue ?
                new ObjectParameter("quantityRequested", quantityRequested) :
                new ObjectParameter("quantityRequested", typeof(int));
    
            var addressNumberParameter = addressNumber.HasValue ?
                new ObjectParameter("addressNumber", addressNumber) :
                new ObjectParameter("addressNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CalculateDeliveryFee_Result>("CalculateDeliveryFee", productNumberParameter, supplierNumberParameter, quantityRequestedParameter, addressNumberParameter);
        }
    
        public virtual ObjectResult<CalculateCourierFee_Result> CalculateCourierFee(Nullable<int> productNumber, Nullable<int> supplierNumber, Nullable<int> quantityRequested, Nullable<int> courierNumber, Nullable<int> addressNumber)
        {
            var productNumberParameter = productNumber.HasValue ?
                new ObjectParameter("productNumber", productNumber) :
                new ObjectParameter("productNumber", typeof(int));
    
            var supplierNumberParameter = supplierNumber.HasValue ?
                new ObjectParameter("supplierNumber", supplierNumber) :
                new ObjectParameter("supplierNumber", typeof(int));
    
            var quantityRequestedParameter = quantityRequested.HasValue ?
                new ObjectParameter("quantityRequested", quantityRequested) :
                new ObjectParameter("quantityRequested", typeof(int));
    
            var courierNumberParameter = courierNumber.HasValue ?
                new ObjectParameter("courierNumber", courierNumber) :
                new ObjectParameter("courierNumber", typeof(int));
    
            var addressNumberParameter = addressNumber.HasValue ?
                new ObjectParameter("addressNumber", addressNumber) :
                new ObjectParameter("addressNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CalculateCourierFee_Result>("CalculateCourierFee", productNumberParameter, supplierNumberParameter, quantityRequestedParameter, courierNumberParameter, addressNumberParameter);
        }
    
        public virtual ObjectResult<GetProduct_Result> GetProduct(Nullable<int> productNumber, Nullable<int> supplierNumber)
        {
            var productNumberParameter = productNumber.HasValue ?
                new ObjectParameter("ProductNumber", productNumber) :
                new ObjectParameter("ProductNumber", typeof(int));
    
            var supplierNumberParameter = supplierNumber.HasValue ?
                new ObjectParameter("SupplierNumber", supplierNumber) :
                new ObjectParameter("SupplierNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetProduct_Result>("GetProduct", productNumberParameter, supplierNumberParameter);
        }
    
        public virtual ObjectResult<GetDetailsForShoppingCart_Result> GetDetailsForShoppingCart(Nullable<int> orderNumber)
        {
            var orderNumberParameter = orderNumber.HasValue ?
                new ObjectParameter("OrderNumber", orderNumber) :
                new ObjectParameter("OrderNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetDetailsForShoppingCart_Result>("GetDetailsForShoppingCart", orderNumberParameter);
        }
    
        public virtual ObjectResult<GetOrderHistory_Result> GetOrderHistory(string customerNumber)
        {
            var customerNumberParameter = customerNumber != null ?
                new ObjectParameter("CustomerNumber", customerNumber) :
                new ObjectParameter("CustomerNumber", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOrderHistory_Result>("GetOrderHistory", customerNumberParameter);
        }
    }
}
