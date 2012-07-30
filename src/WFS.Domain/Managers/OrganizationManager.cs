using System;
using System.Web.Security;
using WFS.Contract.ReqResp;
using WFS.Repository;
using WFS.Repository.Commands;
using WFS.Framework;
using WFS.Contract;
using WFS.Contract.Enums;

namespace WFS.Domain.Managers
{
    public class OrganizationManager
    {
        private WFSRepository _repository;

        public OrganizationManager(WFSRepository repository)
        {
            _repository = repository;
        }

		//public CreateOrganizationResponse CreateOrganization(CreateOrganizationRequest request)
		//{
		//    var resp = new CreateOrganizationResponse();

		//    //check that Membername is OK
		//    MembershipCreateStatus memStat;
		//    var newMem = Membership.CreateUser(request.Email, request.Password, request.Email, null, null, true, out memStat);

		//    if (memStat != MembershipCreateStatus.Success)
		//        return HandleMembershipCreationError(memStat);

		//    //Create WFS User
		//    var wfsComm = new SaveWFSUserCommand((Guid)newMem.ProviderUserKey, request.FirstName, request.LastName, (request.ParentOrgId.HasValue) ? WFSUserTypeEnum.Store.ToString() : WFSUserTypeEnum.Vendor.ToString());
		//    var wfsRes = _repository.ExecuteCommand(wfsComm);

		//    resp.Merge(wfsRes);
		//    if (resp.Status != Status.Success)
		//        return resp;

		//    //Create Vendor and VendorUser
		//    var venCmd = new CreateOrganizationCommand(request.ContactInfo, request.Name, wfsRes.Value.UserId, request.ParentOrgId, request.Type);
		//    var venRes = _repository.ExecuteCommand(venCmd);

		//    resp.Merge(venRes);
		//    if (resp.Status == Status.Success)
		//        resp.Organization = (Vendor)venRes.Value;

		//    return resp;

		//}

		//public static CreateOrganizationResponse HandleMembershipCreationError(MembershipCreateStatus memStat)
		//{
		//    var msg = new Message();

		//    switch (memStat)
		//    {
		//        case MembershipCreateStatus.DuplicateEmail:
		//            msg.Text = "An account with this email already exists";
		//            break;
		//        case MembershipCreateStatus.DuplicateProviderUserKey:
		//            break;
		//        case MembershipCreateStatus.DuplicateUserName:
		//            msg.Text = "An account with this email already exists";
		//            break;
		//        case MembershipCreateStatus.InvalidEmail:
		//            msg.Text = "Your email is not a vaild email address";
		//            break;
		//        case MembershipCreateStatus.InvalidPassword:
		//            msg.Text = "The password was not long enough or contained invalid characters";
		//            break;
		//        case MembershipCreateStatus.InvalidProviderUserKey:
		//            break;
		//        case MembershipCreateStatus.InvalidUserName:
		//            msg.Text = "Your email is not a vaild email address";
		//            break;
		//        default:
		//            msg.Text = "An error occurred please try again";
		//            break;
		//    }

		//    var resp = new CreateOrganizationResponse();
		//    resp.Status = Status.Error;
		//    resp.Messages.Add(msg);
		//    return resp;
		//}

    }
}
