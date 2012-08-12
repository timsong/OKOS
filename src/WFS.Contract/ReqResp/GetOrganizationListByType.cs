using System.Collections.Generic;
using WFS.Contract.Enums;
using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class GetOrganizationByTypeListRequest
    {
        public GetOrganizationByTypeListRequest()
        {
            DataRequest = ActiveDataRequestEnum.All;
        }
        public ActiveDataRequestEnum DataRequest { get; set; }
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
