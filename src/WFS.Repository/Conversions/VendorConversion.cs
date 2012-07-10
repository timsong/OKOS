
using C = WFS.Contract;
using WFS.DataContext;
using System.Data.Entity;

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
                IsActive = data.IsActive,
                Name = data.Name,
                VendorId = data.VendorId,
                Address1 = data.Address1,
                Address2 = data.Address2,
                City = data.City,
                Phone = data.PhoneNumber,
                PhoneExt = data.PhoneExt,
                State = data.State,
                ZipCode = data.ZipCode
            };

            return model;

        }
    }
}
