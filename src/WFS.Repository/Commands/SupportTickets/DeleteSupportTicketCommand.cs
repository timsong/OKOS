using System.Linq;

namespace WFS.Repository.Commands
{
    public class DeleteSupportTicketCommand : ICommand<bool>
    {
        private readonly int _ticketId;

        public DeleteSupportTicketCommand(int ticketId)
        {
            _ticketId = ticketId;
        }


        public IResult<bool> Execute(System.Data.Entity.DbContext dbContext)
        {
            var db = (WFS.DataContext.WFSEntities)dbContext;

            var tick = db.SupportTickets.FirstOrDefault(x => x.TicketId.Equals(_ticketId));
            db.SupportTickets.Remove(tick);

            var i = dbContext.SaveChanges();

            return new Result<bool>((i > 0) ? Status.Success : Status.Error, i > 0);
        }
    }
}
