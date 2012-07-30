using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Repository;
using WFS.Contract.ReqResp;
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

        public GetWfsUserInfoByUserNameResponse  GetWfsUserInfoByUserName (GetWfsUserInfoByUserNameRequest request)
        {
            var response = new GetWfsUserInfoByUserNameResponse();

            var query = new GetWFSUserInfoByUserNameQuery(request.UserName);
            var result = this._repository.ExecuteQuery(query);

            if (result.Status == Status.Success)
                response.UserInfo = result.Value;

            return response;
        }

    }
}
