using System;
using WFS.Contract;
using WFS.Contract.Enums;

namespace WFS.Repository.Conversions.ExtendOrganization
{
    public class ExtendVendor : IExtendedOrganization
    {
        public Contract.Organization Extend(DataContext.Organization data)
        {
            var model = new Vendor()
            {
                OrganizationType = (OrganizationTypeEnum)Enum.Parse(typeof(OrganizationTypeEnum), data.OrganizationType)
            };


            return model;
        }
    }
}
