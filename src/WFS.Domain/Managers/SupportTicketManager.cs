using WFS.Contract.ReqResp;
using WFS.Framework;
using WFS.Repository;
using WFS.Repository.Commands.SupportTickets;
using WFS.Repository.Queries;

namespace WFS.Domain.Managers
{
    public class SupportTicketManager
    {
        private WFSRepository _repository;

        public SupportTicketManager(WFSRepository repository)
        {
            _repository = repository;
        }

        public SaveSupportTicketResponse SaveSupportTicket(SaveSupportTicketRequest request)
        {
            var resp = new SaveSupportTicketResponse();

            var command = new SaveSupportTicketCommand(request.Ticket);
            var result = _repository.ExecuteCommand(command);
            resp.Merge(result);

            if (resp.Status == Status.Success)
                resp.Ticket = result.Value;

            return resp;
        }

        public GetSupportTicketListResponse GetUnresolvedSupportTickets(GetUnResolvedSupportTicketRequest request)
        {
            var response = new GetSupportTicketListResponse();

            var query = new GetUnResolvedSupportTicketsQuery();
            var result = this._repository.ExecuteQuery(query);

            if (result.Status == Status.Success)
                response.Tickets = result.Values;

            return response;
        }
    }
}
