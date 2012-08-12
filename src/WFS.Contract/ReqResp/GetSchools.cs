using System.Collections.Generic;
using WFS.Framework;
using WFS.Contract.Enums;

namespace WFS.Contract.ReqResp
{
    public class GetSchoolsRequest
    {
        public GetSchoolsRequest()
        {
            DataRequest = ActiveDataRequestEnum.All;
        }
        public ActiveDataRequestEnum DataRequest { get; set; }
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
