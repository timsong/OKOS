using System.Collections.Generic;
using WFS.Repository;

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


    public class GetSupportTicketResponse : Result<SupportTicket>
    {
        public SupportTicket Ticket { get; set; }
    }

    public class GetSupportTicketListResponse : ListResult<SupportTicket>
    {
        public GetSupportTicketListResponse()
        {
            Tickets = new List<SupportTicket>();
        }

        public List<SupportTicket> Tickets { get; set; }
    }

}
