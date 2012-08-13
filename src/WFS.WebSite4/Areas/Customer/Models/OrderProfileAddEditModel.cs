using System.Collections.Generic;
using System.Web.Mvc;
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

            Schools = new List<SelectListItem>();
            Grades = new List<SelectListItem>();
            Teachers = new List<SelectListItem>();
            LunchPeriods = new List<SelectListItem>();
        }

        public OrderProfileAddEditModel(OrderProfile profile)
            : this()
        {
            this.Profile = profile;
            this.Status = Status.Success;
        }

        public OrderProfile Profile { get; set; }

        public string SelectedGrade { get; set; }
        public string SelectedTeacher { get; set; }
        public string SelectedLunchPeriod { get; set; }

        public List<SelectListItem> Schools { get; set; }
        public List<SelectListItem> Grades { get; set; }
        public List<SelectListItem> Teachers { get; set; }
        public List<SelectListItem> LunchPeriods { get; set; }

        public bool? IsSchool { get; set; }
        public bool IsNew { get; set; }
    }
}