using System;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    public class GetOrderProfileListRequest
    {
        public Guid MembershipId { get; set; }
    }

    public class GetOrderProfileListResponse : ListResult<OrderProfile>
    {
        public GetOrderProfileListResponse()
        {
            Values = new System.Collections.Generic.List<OrderProfile>();
        }
    }
}
