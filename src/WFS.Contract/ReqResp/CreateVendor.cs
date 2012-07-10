using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFS.Contract.ReqResp
{
    public class CreateVendorRequest
    {
        public CreateVendorRequest()
        {
            ContactInfo = new PhoneAddress();
        }

        public string Name { get; set; }
        public PhoneAddress ContactInfo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

    public class CreateVendorResponse
    {
        public Vendor Vendor { get; set; }
    }

}
