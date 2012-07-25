using System.Collections.Generic;
using WFS.Contract;

namespace WFS.WebSite4.Areas.Admin.Models
{
    public class SchoolsViewModel
    {
        public SchoolsViewModel()
        {
            Schools = new List<Organization>();
        }

        public List<Organization> Schools { get; set; }
    }
}