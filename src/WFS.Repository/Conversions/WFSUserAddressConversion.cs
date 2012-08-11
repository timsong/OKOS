using WFS.DataContext;
using C = WFS.Contract;


namespace WFS.Repository.Conversions
{
    public static class WFSUserAddressConversion
    {
        public static C.PhoneAddress ToDomainModel(this WFSUserAddress data)
        {
            C.PhoneAddress model = new C.PhoneAddress
            {
                Address1 = data.Address1,
                Address2 = data.Address2,
                City = data.City,
                State = data.State,
                ZipCode = data.ZipCode,
                PhoneNumber = data.PhoneNumber,
                PhoneExt = data.PhoneExt
            };
            return model;
        }

        public static WFSUserAddress ToDataModel(this C.PhoneAddress model, int userId)
        {
            WFSUserAddress data = new WFSUserAddress
            {
                Address1 = model.Address1,
                Address2 = model.Address2,
                City = model.City,
                State = model.State,
                ZipCode = model.ZipCode,
                PhoneNumber = model.PhoneNumber,
                PhoneExt = model.PhoneExt,
                UserID = userId
            };

            return data;
        }

        public static void ForUpdate(this WFSUserAddress existing, C.PhoneAddress modified)
        {
            existing.Address1 = modified.Address1;
            existing.Address2 = modified.Address2;
            existing.City = modified.City;
            existing.State = modified.State;
            existing.ZipCode = modified.ZipCode;
            existing.PhoneNumber = modified.PhoneNumber;
            existing.PhoneExt = modified.PhoneExt;
        }


    }
}
