using System;
using WFS.Contract.Enums;
using WFS.DataContext;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class WFSUserConversion
    {
        public static C.WFSUser ToDomainModel(this WFSUser data)
        {
            C.WFSUser model = new C.WFSUser
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                AvailableCredit = data.AvailableCredit,
                EmailAddress = data.User.Membership.Email,
                MembershipGuid = data.MembershipGuid,
                UserId = data.UserId,
                UserType = (C.Enums.WFSUserTypeEnum)Enum.Parse(typeof(C.Enums.WFSUserTypeEnum), data.UserType)
            };
            return model;
        }

        public static WFSUser ToDataModel(this C.WFSUser model)
        {
            WFSUser data = new WFSUser
            {
                FirstName = model.FirstName
                ,
                LastName = model.LastName
                ,
                MembershipGuid = model.MembershipGuid
                    //, Organizations = model.Organizations TODO
                ,
                UserId = model.UserId
                ,
                UserType = model.UserType.ToString()
                ,
                AvailableCredit = model.AvailableCredit
                // , WFSUserAddress = model.WFSUserAddress TODO
            };

            return data;
        }

        public static void ForUpdate(this WFSUser modified, WFSUser existing)
        {
            existing.AvailableCredit = modified.AvailableCredit;

            existing.FirstName = modified.FirstName;

            existing.LastName = modified.LastName;
        }

        public static void ForUpdateMembership(this C.WFSUser model, User existing)
        {
            existing.Membership.Email = model.EmailAddress;
        }

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
