﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WFS.WebSite4.Models;

namespace WFS.WebSite4.Areas.Admin.Models
{
    public class VendorEditModel : IAddressInfo
    {
        #region IAddressInfo Members

        public string Address1 {get; set;}
        public string Address2 {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string ZipCode {get; set;}
        public string PhoneNumber {get; set;}
        public string PhoneExt { get; set; }

        #endregion
    }
}