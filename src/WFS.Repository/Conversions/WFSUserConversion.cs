using System;
using WFS.Contract.Enums;
using WFS.DataContext;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class WFSUserConversion
    {
        public static C.WFSUser ToContract(this WFSUser data)
        {
            if (data == null)
                return null;

            var model = new C.WFSUser()
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                AvailableCredit = data.AvailableCredit,
                MembershipGuid = data.MembershipGuid,
                UserId = data.UserId,
                UserType = (WFSUserTypeEnum)Enum.Parse(typeof(WFSUserTypeEnum), data.UserType),
            };

            return model;
        }
    }
}
