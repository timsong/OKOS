using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WFS.Contract;

namespace WFS.WebSite4.Areas.Admin.Models
{
    public class VendorListViewModel
    {
        public VendorListViewModel()
        {
            Vendors = new List<Organization>();
        }

        public List<Organization> Vendors { get; set; }
    }
}