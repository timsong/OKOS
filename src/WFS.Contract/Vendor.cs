using System;
using System.Collections.Generic;
using WFS.Contract.Enums;

namespace WFS.Contract
{
    public class Vendor
    {
        public Vendor()
        {
            Stores = new List<Store>();
            Menus = new List<Menu>();
        }

        public int VendorId { get; set; }
        public int? ParentVendorId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string PhoneExt { get; set; }

        public OrganizationTypeEnum OrganizationType { get; set; }

        public List<Store> Stores { get; set; }
        public List<Menu> Menus { get; set; }
    }

    public class Store : Vendor
    {
        public int AdvanceDays { get; set; }
        public DateTime CutoffTime { get; set; }
        public int CutoffDay { get; set; }

        public int ParentVendorId { get; set; }
        public bool IsActive { get; set; }
    }
}
