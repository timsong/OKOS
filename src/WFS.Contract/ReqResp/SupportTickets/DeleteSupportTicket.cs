using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class DeleteSupportTicketRequest
    {
        public int TicketId { get; set; }
    }

    public class DeleteSupportTicketResponse : BaseResponse
    {
    }
}
