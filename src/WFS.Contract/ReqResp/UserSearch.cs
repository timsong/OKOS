using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    public class UserSearchRequest
    {
        public string SearchText { get; set; }
        public string RoleFilter { get; set; }
    }

    public class UserSearchResponse : ListResult<WFSUser>
    {
    }
}
