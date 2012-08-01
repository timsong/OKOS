using System.Collections.Generic;
using WFS.Contract;

namespace WFS.WebSite4.Models
{
    public class SupportTicketListViewModel
    {
        public SupportTicketListViewModel()
        {
            Tickets = new List<SupportTicket>();
        }

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
        public SupportTicketNewModel()
        {
            Categories = new List<SelectListItem>();

            Categories.Add(new SelectListItem()
                {
                    Text = "Billing",
                    Value = "Billing"
                });

            Categories.Add(new SelectListItem()
            {
                Text = "Account",
                Value = "Account"
            });

        }

        public SupportTicketNewModel(SupportTicket ticket)
        {
        }

        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactPhone { get; set; }
        public string IssueText { get; set; }

        public string SelectedCategory { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}