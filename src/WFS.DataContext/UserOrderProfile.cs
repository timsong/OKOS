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
    public partial class UserOrderProfile
    {
        public int OrderProfileId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public Nullable<int> TeacherId { get; set; }
        public Nullable<int> SchoolGradeId { get; set; }
        public Nullable<int> LunchPeriodId { get; set; }
        public bool IsActive { get; set; }
        public int OrganizationId { get; set; }
    
        public virtual WFSUser WFSUser { get; set; }
        public virtual Organization Organization { get; set; }
    }
    
}
