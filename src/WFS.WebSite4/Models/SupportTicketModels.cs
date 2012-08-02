using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WFS.Contract;
using WFS.Contract.Enums;

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

    }
    public class SupportTicketNewModel
    {
        public SupportTicketNewModel()
        {
            Categories = new List<SelectListItem>();

            foreach (var item in Enum.GetNames(typeof(SupportCategoryEnum)))
            {
                Categories.Add(new SelectListItem()
                {
                    Text = item,
                    Value = item
                });
            }
        }

        public SupportTicketNewModel(SupportTicket ticket)
        {
        }

        public int? UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string ContactPhone { get; set; }

        [Required]
        public string IssueText { get; set; }

        public string SelectedCategory { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}