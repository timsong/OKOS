using System;
using WFS.Contract.Enums;

namespace WFS.Contract
{
    public class SupportTicket
    {
        public int TicketId { get; set; }
        public SupportCategoryEnum SupportCategory { get; set; }
        public int? CreatedByUserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IssueText { get; set; }
        public DateTime CreatedDate { get; set; }

        public int? ResolvedByUserID { get; set; }
        public string ResolvedText { get; set; }
        public DateTime? ResolvedDate { get; set; }

    }
}
