
using System;
using WFS.Contract.Enums;
using WFS.DataContext;
using WFS.Repository.Conversions.ExtendOrganization;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class SchoolConversion
    {
		static void Map(this C.School School, Organization existing)
		{
			existing.Address1 = School.AddressInfo.Address1;
			existing.Address2 = School.AddressInfo.Address2;
			existing.City = School.AddressInfo.City;
			existing.State = School.AddressInfo.State;
			existing.ZipCode = School.AddressInfo.ZipCode;
			existing.IsActive = School.IsActive;
			existing.Name = School.Name;
			existing.OrganizationType = School.OrganizationType.ToString();
			existing.PhoneExt = School.AddressInfo.PhoneExt;
			existing.PhoneNumber = School.AddressInfo.PhoneNumber;
			existing.UserId = School.User.UserId;
		}

        public static Organization ToDataModel (this C.School domain)
        {
            if (domain == null) return null;

			var data = new Organization { OrganizationId = domain.OrganizationId };

			domain.Map(data);

           

			return data;
        }

		public static void ForUpdate(this Organization existing, C.School School)
		{
			School.Map(existing);
		}

    }
}
