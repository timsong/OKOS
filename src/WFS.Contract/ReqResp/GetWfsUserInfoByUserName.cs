using System;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    public class GetWfsUserInfoByUserNameRequest
    {
        public string UserName { get; set; }
    }

    public class GetWfsUserInfoByIdRequest
    {
        public Guid? MembershipId { get; set; }
        public int? UserId { get; set; }
    }

    public class GetWfsUserInfoResponse : Result<WFSUser>
    {
        public GetWfsUserInfoResponse()
        {
            Value = new WFSUser();
        }
    }
}
