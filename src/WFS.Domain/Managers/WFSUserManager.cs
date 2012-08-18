using WFS.Contract.ReqResp;
using WFS.Framework;
using WFS.Repository;
using WFS.Repository.Queries;
using WFS.Repository.Commands;
using WFS.Contract;

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

        public GetWfsUserInfoResponse GetWfsUserInfoById(GetWfsUserInfoByIdRequest request)
        {
            var response = new GetWfsUserInfoResponse();

            var query = new GetWFSUserInfoByIdQuery(request.MembershipId, request.UserId);
            var result = this._repository.ExecuteQuery(query);

            response.Merge(result);

            if (result.Status == Status.Success)
                response.Value = result.Value;

            return response;
        }


        public SaveWFSUserResponse SaveCustomer(SaveWFSUserRequest request)
        {
            var resp = new SaveWFSUserResponse();
            resp.Value = request.UserInfo;

            var user = request.UserInfo;
            var userCommand = new SaveWFSUserCommand(user);
            var userResponse = _repository.ExecuteCommand(userCommand);

            resp.Merge<WFSUser, WFSUser>((Result<WFSUser>)userResponse);

            if (resp.Status != Status.Success)
                return resp;

            request.UserInfo = userResponse.Value;

            var addCommand = new SaveWfsBillingAddressCommand(request.UserInfo);

            var addRes = _repository.ExecuteCommand(addCommand);

            ((Result<WFSUser>)addRes).Merge<WFSUser, WFSUser>(resp);

            if (resp.Status == Status.Success)
                resp.UserInfo = addRes.Value;

            return resp;
        }
        public SaveWFSUserResponse SaveUserAccountCredits(SaveWFSUserRequest request)
        {
            var resp = new SaveWFSUserResponse();

            var userCommand = new SaveWfsUserAccountCreditCommand(request.UserInfo.UserId, request.UserInfo.AvailableCredit);
            var userResponse = _repository.ExecuteCommand(userCommand);

            resp.Merge<WFSUser, WFSUser>((Result<WFSUser>)userResponse);


            if (resp.Status == Status.Success)
                resp.UserInfo = userResponse.Value;

            return resp;
        }

        public UserSearchResponse GetUserSearchByNameAndFilter(UserSearchRequest request)
        {
            var response = new UserSearchResponse();

            var query = new GetUsersBySearchCriteriaQuery(request.SearchText, request.RoleFilter);
            var result = this._repository.ExecuteQuery(query);

            response.Merge(result);

            if (result.Status == Status.Success)
                response.Values = result.Values;

            return response;
        }
    }
}
