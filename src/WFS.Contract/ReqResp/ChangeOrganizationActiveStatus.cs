using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class ChangeOrganizationActiveStatusRequest
    {
        public int OrganizationID { get; set; }
        public bool NewActiveStatus { get; set; }
    }

    public class ChangeOrganizationActiveStatusResponse : BaseResponse
    {
        public Organization Organization { get; set; }
    }
}
