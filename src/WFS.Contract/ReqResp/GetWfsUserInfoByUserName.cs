using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class GetWfsUserInfoByUserNameRequest
    {
        public string UserName { get; set; }
    }

    public class GetWfsUserInfoByUserNameResponse : BaseResponse
    {
        public GetWfsUserInfoByUserNameResponse()
        {
            UserInfo = new WFSUser();
        }

        public WFSUser UserInfo { get; set; }
    }
}
