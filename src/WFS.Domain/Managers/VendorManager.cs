using System;
using System.Web.Security;
using WFS.Contract.Enums;
using WFS.Contract.ReqResp;
using WFS.Framework;
using WFS.Repository;
using WFS.Repository.Commands;
using WFS.Repository.Queries;

namespace WFS.Domain.Managers
{
    public class VendorManager
    {
        private WFSRepository _repository;

        public VendorManager(WFSRepository repository)
        {
            _repository = repository;
        }

        public GetVendorListResponse GetVendorList(GetVendorListRequest request)
        {
            var response = new GetVendorListResponse();

            var query = new GetVendorListQuery();
            var result = this._repository.ExecuteQuery(query);

            if (result.Status == Status.Success)
                response.Vendors = result.Values;

            return response;
        }
        public GetVendorByIdResponse GetVendorById(GetVendorByIdRequest request)
        {
            var response = new GetVendorByIdResponse();

            var query = new GetVendorByIdQuery(request.VendorID);
            var result = this._repository.ExecuteQuery(query);

            if (result.Status == Status.Success)
                response.Vendor = result.Value;

            return response;
        }

        public ChangeVendorActiveStatusResponse ChangeVendorActiveStatus(ChangeVendorActiveStatusRequest request)
        {
            var resp = new ChangeVendorActiveStatusResponse();

            var command = new ChangeVendorActiveStatusCommand(request.VendorID, request.NewActiveStatus);
            var result = _repository.ExecuteCommand(command);

            if (result.Status == Status.Success)
                resp.Vendor = result.Value;

            return resp;
        }

        public CreateVendorResponse CreateVendor(CreateVendorRequest request)
        {
            var resp = new CreateVendorResponse();

            //check that Membername is OK
            MembershipCreateStatus memStat;
            var newMem = Membership.CreateUser(request.Email, request.Password, request.Email, null, null, true, out memStat);

            if (memStat != MembershipCreateStatus.Success)
                return HandleMembershipCreationError(memStat);

            //Create WFS User
            var wfsComm = new CreateWFSUserCommand((Guid)newMem.ProviderUserKey, request.FirstName, request.LastName, WFSUserTypeEnum.Vendor.ToString());
            var wfsRes = _repository.ExecuteCommand(wfsComm);

            resp.Merge(wfsRes);
            if (resp.Status != Status.Success)
                return resp;

            //Create Vendor and VendorUser
            var venCmd = new CreateVendorAndUserCommand(request.ContactInfo, request.Name, wfsRes.Value.UserId);
            var venRes = _repository.ExecuteCommand(venCmd);

            resp.Merge(venRes);

            return resp;

        }




        public CreateFoodOptionResponse CreateFoodOption(CreateFoodOptionRequest request)
        {
            var resp = new CreateFoodOptionResponse();

            //Create Vendor and VendorUser
            var faCmd = new CreateFoodOptionCommand(request.Name, request.VendorId, request.Description, request.Cost, request.Price);
            var fcRes = _repository.ExecuteCommand(faCmd);

            resp.Merge(fcRes);

            return resp;
        }
        public CreateFoodCategoryResponse CreateFoodCategory(CreateFoodCategoryRequest request)
        {
            var resp = new CreateFoodCategoryResponse();

            //Create Vendor and VendorUser
            var faCmd = new CreateFoodCategoryCommand(request.Name, request.VendorID, request.CategoryType.ToString());
            var fcRes = _repository.ExecuteCommand(faCmd);

            resp.Merge(fcRes);

            return resp;
        }
        private CreateVendorResponse HandleMembershipCreationError(MembershipCreateStatus memStat)
        {
            var msg = new Message();

            switch (memStat)
            {
                case MembershipCreateStatus.DuplicateEmail:
                    msg.Text = "An account with this email already exists";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    msg.Text = "An account with this email already exists";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    msg.Text = "Your email is not a vaild email address";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    msg.Text = "The password was not long enough or contained invalid characters";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    msg.Text = "Your email is not a vaild email address";
                    break;
                default:
                    msg.Text = "An error occurred please try again";
                    break;
            }

            var resp = new CreateVendorResponse();
            resp.Status = Status.Error;
            resp.Messages.Add(msg);
            return resp;
        }
    }
}
