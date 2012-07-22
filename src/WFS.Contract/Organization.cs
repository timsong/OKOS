using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Contract.Enums;
using System.Runtime.Serialization;

namespace WFS.Contract
{
    [Serializable]
    [KnownType(typeof(Vendor))]
    public abstract class Organization
    {
        public int OrganizationId { get; set; }
        public int? ParentOrgId { get; set; }
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
