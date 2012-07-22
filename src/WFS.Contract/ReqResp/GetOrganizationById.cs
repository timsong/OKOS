using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class GetOrganizationByIdRequest
    {
        public int  OrganizationID { get; set; }
    }

    public class GetOrganizationByIdResponse : BaseResponse
    {
        public Organization  Organization { get; set; }
    }
}
