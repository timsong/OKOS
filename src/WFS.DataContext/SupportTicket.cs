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
    public partial class SupportTicket
    {
        public int TicketId { get; set; }
        public string IssueCategory { get; set; }
        public Nullable<int> CreatedUserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IssueText { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ResolvedUserID { get; set; }
        public string ResolvedText { get; set; }
        public Nullable<System.DateTime> ResolvedDate { get; set; }
    
        public virtual WFSUser WFSUser { get; set; }
        public virtual WFSUser WFSUser1 { get; set; }
    }
    
}
