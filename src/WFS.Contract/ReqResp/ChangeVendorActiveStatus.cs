using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class ChangeVendorActiveStatusRequest
    {
        public int VendorID { get; set; }
        public bool NewActiveStatus { get; set; }
    }

    public class ChangeVendorActiveStatusResponse : BaseResponse
    {
        public Vendor Vendor { get; set; }
    }
}
