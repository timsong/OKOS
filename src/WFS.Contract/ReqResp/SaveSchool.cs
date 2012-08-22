using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    public class SaveSchoolRequest
    {
        public School Subject { get; set; }
    }

    public class SaveSchoolResponse : Result<School>
    {
    }
}
