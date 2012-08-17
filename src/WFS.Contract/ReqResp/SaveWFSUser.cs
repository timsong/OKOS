using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    public class SaveWFSUserRequest
    {
        public WFSUser UserInfo { get; set; }
    }

    public class SaveWFSUserResponse : Result<WFSUser>
    {
        public WFSUser UserInfo { get; set; }
    }
}
