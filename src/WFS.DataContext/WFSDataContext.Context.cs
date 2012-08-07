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
    
        public DbSet<FoodItemOption> FoodItemOptions { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<WFSUser> WFSUsers { get; set; }
        public DbSet<WFSUserAddress> WFSUserAddresses { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<VendorFoodCategory> VendorFoodCategories { get; set; }
        public DbSet<VendorFoodOption> VendorFoodOptions { get; set; }
        public DbSet<VendorMenu> VendorMenus { get; set; }
        public DbSet<SupportTicket> SupportTickets { get; set; }
        public DbSet<DaysOff> DaysOffs { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<LunchPeriod> LunchPeriods { get; set; }
        public DbSet<SchoolDaysOff> SchoolDaysOffs { get; set; }
        public DbSet<SchoolGrade> SchoolGrades { get; set; }
        public DbSet<SchoolLunchPeriod> SchoolLunchPeriods { get; set; }
        public DbSet<SchoolTeacher> SchoolTeachers { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}
