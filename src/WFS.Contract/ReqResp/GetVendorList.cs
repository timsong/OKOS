using System.Collections.Generic;
using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class GetVendorListRequest
    {
    }

    public class GetVendorListResponse : BaseResponse
    {
        public GetVendorListResponse()
        {
            Vendors = new List<Vendor>();
        }

        public List<Vendor> Vendors { get; set; }


    }
}
