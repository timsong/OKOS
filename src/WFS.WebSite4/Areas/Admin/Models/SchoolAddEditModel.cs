using WFS.Contract;
using WFS.Framework;
using System;
using System.Globalization;

namespace WFS.WebSite4.Areas.Admin.Models
{
    public class SchoolAddEditModel : EditModelBase<School>
    {
        public SchoolAddEditModel()
            : base()
        {
            this.School = new School();
            this.DeliveryTime = DateTime.ParseExact(new TimeSpan(10,0,0).ToString(), "HH:mm:ss", CultureInfo.CurrentCulture).ToString("hh:mm tt");
            this.Status = Status.Success;
        }

        public SchoolAddEditModel(School school)
        {
            this.School = school;
            this.DeliveryTime = DateTime.ParseExact(school.DeliveryTime.ToString(), "HH:mm:ss", CultureInfo.CurrentCulture).ToString("hh:mm tt");
            this.Status = Status.Success;
        }

        public School School { get; set; }
        public string DeliveryTime { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}