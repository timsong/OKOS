using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFS.Contract
{
    public class Store
    {
        public int StoreId { get; set; }
        public string Name { get; set; }

        public int AdvanceDays { get; set; }
        public DateTime CutoffTime { get; set; }
        public int CutoffDay { get; set; }

        public int VendorId { get; set; }
        public bool IsActive { get; set; }
    }
}
