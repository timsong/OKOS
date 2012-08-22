
using System;
using WFS.Contract.Enums;
using WFS.DataContext;
using WFS.Repository.Conversions.ExtendOrganization;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class VendorConversion
    {
		static void Map(this C.Vendor vendor, Organization existing)
		{
			existing.Address1 = vendor.AddressInfo.Address1;
			existing.Address2 = vendor.AddressInfo.Address2;
			existing.City = vendor.AddressInfo.City;
			existing.State = vendor.AddressInfo.State;
			existing.ZipCode = vendor.AddressInfo.ZipCode;
			existing.IsActive = vendor.IsActive;
			existing.Name = vendor.Name;
			existing.OrganizationType = vendor.OrganizationType.ToString();
			existing.PhoneExt = vendor.AddressInfo.PhoneExt;
			existing.PhoneNumber = vendor.AddressInfo.PhoneNumber;
			existing.UserId = vendor.User.UserId;
		}

        public static Organization ToDataModel (this C.Vendor domain)
        {
            if (domain == null) return null;

			var data = new Organization { OrganizationId = domain.OrganizationId };


			return data;
        }

		public static void ForUpdate(this Organization existing, C.Vendor vendor)
		{
			vendor.Map(existing);
		}

    }
}
