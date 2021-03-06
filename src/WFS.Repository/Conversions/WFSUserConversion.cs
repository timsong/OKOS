﻿using System;
using WFS.Contract.Enums;
using WFS.DataContext;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class WFSUserConversion
    {

        public static WFSUser ToDataModel(this C.WFSUser model)
        {
            WFSUser data = new WFSUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MembershipGuid = model.MembershipGuid,
                UserId = model.UserId,
                UserType = model.UserType.ToString(),
                AvailableCredit = model.AvailableCredit
            };

            return data;
        }

        public static void ForUpdate(this WFSUser modified, WFSUser existing)
        {
            existing.FirstName = modified.FirstName;
            existing.LastName = modified.LastName;
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
                EmailAddress = data.User.Membership.Email,
                Username = data.User.UserName,
                UserId = data.UserId,
                UserType = (WFSUserTypeEnum)Enum.Parse(typeof(WFSUserTypeEnum), data.UserType),
            };

            if (data.WFSUserAddress != null)
                model.BillingAddress = data.WFSUserAddress.ToContract();

            return model;
        }
    }
}
