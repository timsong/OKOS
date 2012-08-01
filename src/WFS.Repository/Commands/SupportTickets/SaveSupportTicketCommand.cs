using System;
using System.Data.Entity;
using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;


namespace WFS.Repository.Commands.SupportTickets
{
    public class SaveSupportTicketCommand : ICommand<C.SupportTicket>
    {
        private readonly C.SupportTicket _supportTicket;

        public SaveSupportTicketCommand(C.SupportTicket supportTicket)
        {
            _supportTicket = supportTicket;
        }

        public IResult<C.SupportTicket> Execute(DbContext dbContext)
        {
            var context = (WFS.DataContext.WFSEntities)dbContext;
            var result = new Result<C.SupportTicket>();

            try
            {
                if (_supportTicket.TicketId > 0)
                {
                    var tick = context.SupportTickets.FirstOrDefault(x => x.TicketId.Equals(_supportTicket.TicketId));

                    tick.ForUpdate(_supportTicket);
                }
                else
                {
                    context.SupportTickets.Add(_supportTicket.ToDataModel());
                }

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Status = Status.Error;
                result.Messages.Add(new Message { Code = "SaveSupportTicketFailed", Text = ex.ToString() });
            }

            return result;
        }


    }
}
