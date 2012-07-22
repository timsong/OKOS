﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WFS.Contract.Enums;
using WFS.Contract.Interfaces;

namespace WFS.Contract
{
    [Serializable]
    [KnownType(typeof(Vendor))]
    public abstract class Organization
    {
        public Organization()
        {
            User = new WFSUser();
            AddressInfo = new PhoneAddress();
        }

        public int OrganizationId { get; set; }
        public int? ParentOrgId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public PhoneAddress AddressInfo { get; set; }

        public OrganizationTypeEnum OrganizationType { get; set; }
        public WFSUser User { get; set; }
    }

    public class Vendor : Organization
    {
        public Vendor()
        {
            Stores = new List<Store>();
            Menus = new List<Menu>();
            OrganizationType = OrganizationTypeEnum.Vendor;
        }

        public List<Store> Stores { get; set; }
        public List<Menu> Menus { get; set; }
    }

    public class Store : Vendor
    {
        public int AdvanceDays { get; set; }
        public DateTime CutoffTime { get; set; }
        public int CutoffDay { get; set; }
    }


}
