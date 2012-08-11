
using System;
using System.Collections.Generic;
using WFS.Contract;

namespace WFS.WebSite4.Areas.Customer.Models
{
    public class OrderProfileViewModel
    {
        public OrderProfileViewModel()
        {
            Profiles = new List<OrderProfile>();
        }

        public Guid MembershipId { get; set; }

        public List<OrderProfile> Profiles { get; set; }
    }
}