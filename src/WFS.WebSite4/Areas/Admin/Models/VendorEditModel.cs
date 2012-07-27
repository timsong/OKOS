using C = WFS.Contract;
using WFS.Framework;

namespace WFS.WebSite4.Areas.Admin.Models
{
    public class VendorEditModel : EditModelBase<C.Vendor>
    {
        public VendorEditModel() : base()
        {
            Vendor = new C.Vendor();

			Status = WFS.Status.Success;
        }
		
		public VendorEditModel(C.Vendor vendor)
		{
			Vendor = vendor;

			Status = WFS.Status.Success;
		}


        public C.Vendor Vendor { get; set; }

        public bool IsNew { get; set; }

    }
}