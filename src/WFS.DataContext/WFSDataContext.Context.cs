﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace WFS.DataContext
{
    public partial class WFSEntities : DbContext
    {
        public WFSEntities()
            : base("name=WFSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerUser> CustomerUsers { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<FoodItemOption> FoodItemOptions { get; set; }
        public DbSet<FoodOption> FoodOptions { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreUser> StoreUsers { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorUser> VendorUsers { get; set; }
        public DbSet<WFSUser> WFSUsers { get; set; }
        public DbSet<WFSUserAddress> WFSUserAddresses { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
