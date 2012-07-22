using System.Collections.Generic;
using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class GetOrganizationByTypeListRequest
    {
        public Enums.OrganizationTypeEnum Type { get; set; }
    }

    public class GetOrganizationListByTypeResponse : BaseResponse
    {
        public GetOrganizationListByTypeResponse()
        {
            Organizations = new List<Organization>();
        }

        public List<Organization> Organizations { get; set; }


    }
}
