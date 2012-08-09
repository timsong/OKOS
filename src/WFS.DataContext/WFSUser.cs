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
    public partial class WFSUser
    {
        public WFSUser()
        {
            this.Organizations = new HashSet<Organization>();
            this.SupportTickets = new HashSet<SupportTicket>();
            this.SupportTickets1 = new HashSet<SupportTicket>();
            this.UserOrderProfiles = new HashSet<UserOrderProfile>();
        }
    
        public int UserId { get; set; }
        public System.Guid MembershipGuid { get; set; }
        public string UserType { get; set; }
        public decimal AvailableCredit { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    
        public virtual WFSUserAddress WFSUserAddress { get; set; }
        public virtual ICollection<Organization> Organizations { get; set; }
        public virtual ICollection<SupportTicket> SupportTickets { get; set; }
        public virtual ICollection<SupportTicket> SupportTickets1 { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<UserOrderProfile> UserOrderProfiles { get; set; }
    }
    
}
