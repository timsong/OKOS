using System;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Commands
{
    public class CreateVendorAndUserCommand : ICommand<C.Vendor>
    {
        private readonly C.PhoneAddress _addInfo;
        private readonly string _name;
        private readonly int _userId;
        private readonly int? _parentVendorId;

        public CreateVendorAndUserCommand(C.PhoneAddress addInfo, string name, int userId, int? parentVendorId)
        {
            _addInfo = addInfo;
            _name = name;
            _userId = userId;
            _parentVendorId = parentVendorId;
        }
        #region ICommand<Vendor> Members

        public IResult<C.Vendor> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;

            var vend = new WFS.DataContext.Vendor()
            {
                Address1 = _addInfo.Address1,
                Address2 = _addInfo.Address2,
                City = _addInfo.City,
                Name = _name,
                State = _addInfo.State,
                PhoneNumber = _addInfo.PhoneNumber,
                PhoneExt = _addInfo.PhoneExt,
                ZipCode = _addInfo.ZipCode,
                IsActive = true,
                ParentVendorId = _parentVendorId,
            };

            try
            {
                vend.VendorUsers.Add(new DataContext.VendorUser()
                    {
                        UserId = _userId,
                    });

                ent.Vendors.Add(vend);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var fail = new Result<C.Vendor>(Status.Error);
                fail.Messages.Add(new Message() { Text = ex.Message });
                return fail;
            }

            var result = new Result<C.Vendor>(Status.Success, vend.ToContract());
            return result;
        }

        #endregion

    }
}
