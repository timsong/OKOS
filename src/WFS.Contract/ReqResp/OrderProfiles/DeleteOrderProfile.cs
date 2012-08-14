using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    public class DeleteOrderProfileRequest
    {
        public int ProfileId { get; set; }
    }

    public class DeleteOrderProfileResponse : Result<bool>
    {
    }
}
