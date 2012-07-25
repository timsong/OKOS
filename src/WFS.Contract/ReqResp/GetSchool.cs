using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class GetSchoolRequest
    {
        public int SchoolID { get; set; }
    }

    public class GetSchoolResponse : BaseResponse
    {
        public Organization School { get; set; }
    }
}
