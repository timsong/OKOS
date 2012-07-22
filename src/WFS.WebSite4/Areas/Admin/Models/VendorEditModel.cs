using C = WFS.Contract;

namespace WFS.WebSite4.Areas.Admin.Models
{
    public class VendorEditModel
    {

        public VendorEditModel()
        {
            Vendor = new C.Vendor();
        }

        public C.Vendor Vendor { get; set; }

        public bool IsNew { get; set; }

    }
}