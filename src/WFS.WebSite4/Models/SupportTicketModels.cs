using System.Collections.Generic;
using WFS.Contract;

namespace WFS.WebSite4.Models
{
    public class SupportTicketListViewModel
    {
        public List<SupportTicket> Tickets { get; set; }
    }

    public class SupportTicketEditModel
    {

        public SupportTicketEditModel()
        {
            Ticket = new SupportTicket();
        }

        public SupportTicketEditModel(SupportTicket ticket)
        {
            Ticket = ticket;
        }


        public SupportTicket Ticket { get; set; }

        public bool IsNew { get; set; }
    }
    public class SupportTicketNewModel
    {
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactPhone { get; set; }
        public string IssueText { get; set; }
    }
}