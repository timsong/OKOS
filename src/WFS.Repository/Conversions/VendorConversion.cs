
using WFS.Contract.Enums;
using WFS.DataContext;
using C = WFS.Contract;
using System;

namespace WFS.Repository.Conversions
{
    public static class VendorConversion
    {
        public static C.Vendor ToContract(this Organization data)
        {
            if (data == null)
                return null;

            var model = new C.Vendor()
            {
                OrganizationId = data.OrganizationId,
                IsActive = data.IsActive,
                Name = data.Name,
                Address1 = data.Address1,
                Address2 = data.Address2,
                City = data.City,
                Phone = data.PhoneNumber,
                PhoneExt = data.PhoneExt,
                State = data.State,
                ZipCode = data.ZipCode,
                ParentOrgId = data.ParentOrgId,
                OrganizationType = (OrganizationTypeEnum)Enum.Parse(typeof(OrganizationTypeEnum), data.OrganizationType)
            };


            return model;

        }
    }
}
