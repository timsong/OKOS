using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class GetVendorByIdRequest
    {
        public int VendorID { get; set; }
    }

    public class GetVendorByIdResponse : BaseResponse
    {
        public Vendor Vendor { get; set; }
    }
}
