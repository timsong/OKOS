//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace WFS.DataContext
{
    public partial class Organization
    {
        public Organization()
        {
            this.VendorFoodCategories = new HashSet<VendorFoodCategory>();
            this.VendorFoodOptions = new HashSet<VendorFoodOption>();
            this.Organization1 = new HashSet<Organization>();
            this.VendorMenus = new HashSet<VendorMenu>();
            this.SchoolDaysOffs = new HashSet<SchoolDaysOff>();
            this.SchoolGrades = new HashSet<SchoolGrade>();
            this.SchoolLunchPeriods = new HashSet<SchoolLunchPeriod>();
            this.SchoolTeachers = new HashSet<SchoolTeacher>();
        }
    
        public int OrganizationId { get; set; }
        public Nullable<int> ParentOrgId { get; set; }
        public string OrganizationType { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneExt { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<VendorFoodCategory> VendorFoodCategories { get; set; }
        public virtual ICollection<VendorFoodOption> VendorFoodOptions { get; set; }
        public virtual WFSUser WFSUser { get; set; }
        public virtual ICollection<Organization> Organization1 { get; set; }
        public virtual Organization Organization2 { get; set; }
        public virtual ICollection<VendorMenu> VendorMenus { get; set; }
        public virtual ICollection<SchoolDaysOff> SchoolDaysOffs { get; set; }
        public virtual ICollection<SchoolGrade> SchoolGrades { get; set; }
        public virtual ICollection<SchoolLunchPeriod> SchoolLunchPeriods { get; set; }
        public virtual ICollection<SchoolTeacher> SchoolTeachers { get; set; }
    }
    
}
