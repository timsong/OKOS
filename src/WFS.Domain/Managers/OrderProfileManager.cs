using WFS.Contract.ReqResp;
using WFS.Framework;
using WFS.Repository;
using WFS.Repository.Commands;
using WFS.Repository.Queries;

namespace WFS.Domain.Managers
{
    public class OrderProfileManager
    {
        private WFSRepository _repository;

        public OrderProfileManager(WFSRepository repository)
        {
            _repository = repository;
        }

        public GetOrderProfileResponse GetOrderProfileById(GetOrderProfileByIdRequest request)
        {
            var response = new GetOrderProfileResponse();

            var query = new GetOrderProfileByIdQuery(request.ProfileId);
            var result = this._repository.ExecuteQuery(query);

            response.Merge(result);
            if (result.Status == Status.Success)
                response.Value = result.Value;

            return response;
        }
        public GetOrderProfileListResponse GetListOfProfiles(GetOrderProfileListRequest request)
        {
            var resp = new GetOrderProfileListResponse();

            var q = new GetOrderProfileListByUserIdQuery(request.UserId);
            var result = _repository.ExecuteQuery(q);

            resp.Merge(result);

            if (resp.Status == Status.Success)
                resp.Values = result.Values;

            return resp;
        }

        public SaveOrderProfileResponse SaveOrderProfile(SaveOrderProfileRequest request)
        {
            var resp = new SaveOrderProfileResponse();

            var command = new SaveOrderProfileCommand(request.Profile);
            var result = _repository.ExecuteCommand(command);
            resp.Merge(result);

            if (resp.Status == Status.Success)
                resp.Value = result.Value;

            return resp;
        }


    }
}
