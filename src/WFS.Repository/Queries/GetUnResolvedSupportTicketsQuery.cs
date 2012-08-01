using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Queries
{
    public class GetUnResolvedSupportTicketsQuery : IListQuery<C.SupportTicket>
    {
        public GetUnResolvedSupportTicketsQuery()
        {
        }

        IListResult<C.SupportTicket> IListQuery<C.SupportTicket>.Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;

            var ticks = from fi in ent.SupportTickets
                       where fi.ResolvedDate == null
                       orderby fi.CreatedDate, fi.TicketId
                       select fi;

            var data = ticks.AsEnumerable().Select(x => x.ToContract());

            var result = new ListResult<C.SupportTicket>(data.ToList());
            result.Status = Status.Success;
            return result;
        }
    }
}
