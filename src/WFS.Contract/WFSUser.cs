using System;
using WFS.Contract.Enums;

namespace WFS.Contract
{
    public class WFSUser
    {
        public int UserId { get; set; }
        public Guid MembershipGuid { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public WFSUserTypeEnum UserType { get; set; }

        public decimal AvailableCredit { get; set; }

    }
}
