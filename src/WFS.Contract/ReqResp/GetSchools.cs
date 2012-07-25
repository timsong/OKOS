using System.Collections.Generic;
using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class GetSchoolsRequest
    {
    }

    public class GetSchoolsResponse : BaseResponse
    {
        public GetSchoolsResponse()
        {
            Schools = new List<Organization>();
        }

        public List<Organization> Schools { get; set; }
    }
}
