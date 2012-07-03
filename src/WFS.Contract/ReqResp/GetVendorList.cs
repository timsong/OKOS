using System.Collections.Generic;

namespace WFS.Contract.ReqResp
{
    public class GetVendorListRequest
    {
    }

    public class GetVendorListResponse
    {
        public GetVendorListResponse()
        {
            Vendors = new List<Vendor>();
        }

        public List<Vendor> Vendors { get; set; }

    }
}
