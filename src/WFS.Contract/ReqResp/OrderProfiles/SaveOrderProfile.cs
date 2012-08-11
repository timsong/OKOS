using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    public class SaveOrderProfileRequest
    {
        public OrderProfile Profile { get; set; }
    }

    public class SaveOrderProfileResponse : Result<OrderProfile>
    {
    }
}
