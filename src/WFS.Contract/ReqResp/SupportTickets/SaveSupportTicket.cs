using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class SaveSupportTicketRequest
    {
        public SupportTicket Ticket { get; set; }
    }

    public class SaveSupportTicketResponse : BaseResponse
    {
        public SupportTicket Ticket { get; set; }
    }
}
