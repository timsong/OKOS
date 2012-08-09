using WFS.Contract.ReqResp;
using WFS.Framework;
using WFS.Repository;
using WFS.Repository.Commands;

namespace WFS.Domain.Managers
{
    public class OrderProfileManager
    {
        private WFSRepository _repository;

        public OrderProfileManager(WFSRepository repository)
        {
            _repository = repository;
        }

        public GetOrderProfileListResponse GetListOfProfiles(GetOrderProfileListRequest request)
        {
            return new GetOrderProfileListResponse();
        }

        public SaveOrderProfileResponse SaveOrderProfile(SaveOrderProfileRequest request)
        {
            var resp = new SaveOrderProfileResponse();

            var command = new SaveOrderProfileCommand(request.Ticket);
            var result = _repository.ExecuteCommand(command);
            resp.Merge(result);

            if (resp.Status == Status.Success)
                resp.Value = result.Value;

            return resp;
        }
    }
}
