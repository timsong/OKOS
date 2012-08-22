
using System;
using WFS.Contract.Enums;
using WFS.DataContext;
using WFS.Repository.Conversions.ExtendOrganization;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class SchoolConversion
    {
		static void Map(this C.School School, School existing)
		{
			existing.Organization.Address1 = School.AddressInfo.Address1;
            existing.Organization.Address2 = School.AddressInfo.Address2;
            existing.Organization.City = School.AddressInfo.City;
            existing.Organization.State = School.AddressInfo.State;
            existing.Organization.ZipCode = School.AddressInfo.ZipCode;
            existing.Organization.IsActive = School.IsActive;
            existing.Organization.Name = School.Name;
            existing.Organization.OrganizationType = School.OrganizationType.ToString();
            existing.Organization.PhoneExt = School.AddressInfo.PhoneExt;
            existing.Organization.PhoneNumber = School.AddressInfo.PhoneNumber;
            existing.Organization.UserId = School.User.UserId;
            existing.DeliveryTime = School.DeliveryTime;
		}

        public static School ToDataModel(this C.School domain)
        {
            if (domain == null) return null;

            var data = new School { SchoolId = domain.OrganizationId };
            data.Organization = new Organization();
			domain.Map(data);

			return data;
        }

		public static void ForUpdate(this School existing, C.School School)
		{
			School.Map(existing);
		}

        public static C.School ToContract(this School data)
        {
            if (data == null)
                return null;

            var model = new C.School();

            model.OrganizationId = data.Organization.OrganizationId;
            model.IsActive = data.Organization.IsActive;
            model.Name = data.Organization.Name;
            model.AddressInfo.Address1 = data.Organization.Address1;
            model.AddressInfo.Address2 = data.Organization.Address2;
            model.AddressInfo.City = data.Organization.City;
            model.AddressInfo.PhoneNumber = data.Organization.PhoneNumber;
            model.AddressInfo.PhoneExt = data.Organization.PhoneExt;
            model.AddressInfo.State = data.Organization.State;
            model.AddressInfo.ZipCode = data.Organization.ZipCode;
            model.ParentOrgId = data.Organization.ParentOrgId;

            model.User = data.Organization.WFSUser.ToContract();
            model.DeliveryTime = data.DeliveryTime;

            return model;

        }


    }
}
