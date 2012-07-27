
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
            
			model.AddressInfo.Address1 = data.Address1;
            
			model.AddressInfo.Address2 = data.Address2;
            
			model.AddressInfo.City = data.City;
            
			model.AddressInfo.PhoneNumber = data.PhoneNumber;
            
			model.AddressInfo.PhoneExt = data.PhoneExt;
            
			model.AddressInfo.State = data.State;
            
			model.AddressInfo.ZipCode = data.ZipCode;
            
			model.ParentOrgId = data.ParentOrgId;

			model.User = data.WFSUser.ToDomainModel();

            return model;

        }
    }
}
