﻿
using WFS.DataContext;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class VendorConversion
    {
        public static C.Vendor ToContract(this Vendor data)
        {
            if (data == null)
                return null;

            var model = new C.Vendor()
            {
                VendorId = data.VendorId,
                IsActive = data.IsActive,
                Name = data.Name,
                Address1 = data.Address1,
                Address2 = data.Address2,
                City = data.City,
                Phone = data.PhoneNumber,
                PhoneExt = data.PhoneExt,
                State = data.State,
                ZipCode = data.ZipCode,
            };

            return model;

        }
    }
}
