using WFS.Contract.ReqResp;
using WFS.Framework;
using WFS.Repository;
using WFS.Repository.Queries;

namespace WFS.Domain.Managers
{
    public class WFSUserManager
    {
        private WFSRepository _repository;

        public WFSUserManager(WFSRepository repository)
        {
            _repository = repository;
        }

        public GetWfsUserInfoResponse GetWfsUserInfoByUserName(GetWfsUserInfoByUserNameRequest request)
        {
            var response = new GetWfsUserInfoResponse();

            var query = new GetWFSUserInfoByUserNameQuery(request.UserName);
            var result = this._repository.ExecuteQuery(query);

            response.Merge(result);

            if (result.Status == Status.Success)
                response.Value = result.Value;

            return response;
        }

        public GetWfsUserInfoResponse GetWfsUserInfoByMembershipId(GetWfsUserInfoByMembershipIdRequest request)
        {
            var response = new GetWfsUserInfoResponse();

            var query = new GetWFSUserInfoByMembershipIdQuery(request.MembershipId);
            var result = this._repository.ExecuteQuery(query);

            response.Merge(result);

            if (result.Status == Status.Success)
                response.Value = result.Value;

            return response;
        }
    }
}
