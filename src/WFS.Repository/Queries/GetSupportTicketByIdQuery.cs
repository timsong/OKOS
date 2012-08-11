using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Queries
{
    public class GetSupportTicketByIdQuery : IQuery<C.SupportTicket>
    {
        public int _supportTicketId { get; set; }

        public GetSupportTicketByIdQuery(int supportTicketId)
        {
            _supportTicketId = supportTicketId;
        }


        public IResult<C.SupportTicket> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var data = (from x in ent.SupportTickets
                        where x.TicketId == _supportTicketId
                        select x).FirstOrDefault().ToContract();

            var result = new Result<C.SupportTicket>(Status.Success, data);

            return result;
        }
    }
}

