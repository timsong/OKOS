
using WFS.Framework;
namespace WFS.Contract.ReqResp
{
    public class CreateVendorRequest
    {
        public CreateVendorRequest()
        {
            ContactInfo = new PhoneAddress();
        }

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public PhoneAddress ContactInfo { get; set; }
        public int? ParentVendorId { get; set; }


    }

    public class CreateVendorResponse : BaseResponse
    {
        public Vendor Vendor { get; set; }
    }

}
