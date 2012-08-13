using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    public class GetOrderProfileListRequest
    {
        public int UserId { get; set; }
    }

    public class GetOrderProfileByIdRequest
    {
        public int ProfileId { get; set; }
    }

    public class GetOrderProfileListResponse : ListResult<OrderProfile>
    {
        public GetOrderProfileListResponse()
        {
            Values = new System.Collections.Generic.List<OrderProfile>();
        }

        public int UserId { get; set; }
    }

    public class GetOrderProfileResponse : Result<OrderProfile>
    {
        public GetOrderProfileResponse()
        {
            Value = new OrderProfile();
        }
    }


}
