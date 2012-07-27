using System;
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
}