
using WFS.Framework;
namespace WFS.Contract.ReqResp
{
    public class CreateOrganizationRequest
    {
        public CreateOrganizationRequest()
        {
            ContactInfo = new PhoneAddress();
        }

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public PhoneAddress ContactInfo { get; set; }
        public int? ParentOrgId { get; set; }

        public Enums.OrganizationTypeEnum Type { get; set; }


    }

    public class CreateOrganizationResponse : BaseResponse
    {
        public Organization Organization { get; set; }
    }

}
