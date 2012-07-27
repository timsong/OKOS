using System.Collections.Generic;
using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class GetSupportTicketByIDRequest
    {
        public int TicketID { get; set; }
    }

    public class GetSupportTicketByCreatorIDRequest
    {
        public int UserId { get; set; }

    }

    public class GetUnResolvedSupportTicketRequest
    {
    }


    public class GetSupportTicketResponse : BaseResponse
    {
        public SupportTicket Ticket { get; set; }
    }

    public class GetSupportTicketListResponse : BaseResponse
    {
        public GetSupportTicketListResponse()
        {
            Tickets = new List<SupportTicket>();
        }

        public List<SupportTicket> Tickets { get; set; }
    }

}
