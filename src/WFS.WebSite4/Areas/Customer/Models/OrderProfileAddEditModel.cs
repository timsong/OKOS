using System.Collections.Generic;
using WFS.Contract;
using WFS.Framework;

namespace WFS.WebSite4.Areas.Customer.Models
{
    public class OrderProfileAddEditModel : EditModelBase<OrderProfile>
    {
        public OrderProfileAddEditModel()
            : base()
        {
            this.Profile = new OrderProfile();
            this.Status = Status.Success;
        }

        public OrderProfileAddEditModel(OrderProfile profile)
        {
            this.Profile = profile;
            this.Status = Status.Success;
        }

        public OrderProfile Profile { get; set; }

        public List<SelectListItem> Grades { get; set; }
        public List<SelectListItem> Teachers { get; set; }
        public List<SelectListItem> LunchPeriods { get; set; }

        public bool? IsSchool { get; set; }
        public bool? IsSelf { get; set; }
        public bool IsNew { get; set; }
        public bool IsComplete { get; set; }

    }
}