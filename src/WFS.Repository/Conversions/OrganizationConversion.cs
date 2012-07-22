﻿
using System;
using WFS.Contract.Enums;
using WFS.DataContext;
using WFS.Repository.Conversions.ExtendOrganization;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class OrganizationConversion
    {
        public static C.Organization ToContract(this Organization data)
        {
            if (data == null)
                return null;

            var type = (OrganizationTypeEnum)Enum.Parse(typeof(OrganizationTypeEnum), data.OrganizationType);
            var extClass = OrganizationExtensionFactory.GetExtensionClass(type);

            var model = extClass.Extend(data);

            model.OrganizationId = data.OrganizationId;
            model.IsActive = data.IsActive;
            model.Name = data.Name;
            model.Address1 = data.Address1;
            model.Address2 = data.Address2;
            model.City = data.City;
            model.Phone = data.PhoneNumber;
            model.PhoneExt = data.PhoneExt;
            model.State = data.State;
            model.ZipCode = data.ZipCode;
            model.ParentOrgId = data.ParentOrgId;


            return model;

        }
    }
}
