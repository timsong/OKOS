using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Repository;
using WFS.Contract.ReqResp;
using WFS.Repository.Commands;
using WFS.Framework;
using WFS.Contract;

namespace WFS.Domain.Managers
{
    public class CustomerManager
    {
        private WFSRepository _repository;

        public CustomerManager(WFSRepository repository)
        {
            _repository = repository;
        }


        public CreateCustomerAccountResponse SaveCustomer(CreateCustomerAccountRequest request)
        {
            var resp = new CreateCustomerAccountResponse();
            resp.Value = request.AccountInfo;

            var user = request.AccountInfo.User;
            var userCommand = new SaveWFSUserCommand(user);
            var userResponse = _repository.ExecuteCommand(userCommand);

            resp.Merge<CustomerAccount, WFSUser>((Result<WFSUser>)userResponse);

            if (resp.Status != Status.Success)
                return resp;

            request.AccountInfo.User = userResponse.Value;

            var addCommand = new SaveWfsBillingAddressCommand(request.AccountInfo);

            var addRes = _repository.ExecuteCommand(addCommand);

            ((Result<CustomerAccount>)addRes).Merge<CustomerAccount, CustomerAccount>(resp);

            if (resp.Status == Status.Success)
                resp.AccountInfo = addRes.Value;

            return resp;
        }



    }
}
