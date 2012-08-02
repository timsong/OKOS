using WFS.Framework;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    public class GetSchoolRequest
    {
        public int SchoolID { get; set; }
    }

    public class GetSchoolResponse : Result<School>
    {
    }
}
