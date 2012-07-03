using System;

namespace WFS.Contract
{
    public class WFSUser
    {
        public int UserId { get; set; }
        public Guid MembershipGuid { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string ZipCode { get; set; }

        public string PhoneNumber { get; set; }
        public string PhoneExt { get; set; }

        public decimal AvailableCredit { get; set; }


    }
}
