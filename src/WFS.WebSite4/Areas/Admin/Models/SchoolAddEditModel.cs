using System.Collections.Generic;
using WFS.Contract;
using WFS.Framework;

namespace WFS.WebSite4.Areas.Admin.Models
{
    public class SchoolAddEditModel : EditModelBase<School>
    {
        public SchoolAddEditModel() : base()
        {
            this.School = new School();
			this.Status = Status.Success;
        }

        public SchoolAddEditModel(School school)
		{
            this.School = school;
			this.Status = Status.Success;
		}

        public School School { get; set; }
        public bool IsNew { get; set; }
    }
}