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
            Vendors = new List<Vendor>();
        }

        public List<Vendor> Vendors { get; set; }
    }
}