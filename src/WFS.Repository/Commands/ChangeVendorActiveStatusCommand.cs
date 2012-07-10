using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C = WFS.Contract;
using WFS.Repository.Conversions;

namespace WFS.Repository.Commands
{
    public class ChangeVendorActiveStatusCommand : ICommand<C.Vendor>
    {
        private readonly  int _vendorId;
        private readonly bool _isActive;

        public ChangeVendorActiveStatusCommand(int vendorId, bool isActive)
        {
            _vendorId = vendorId;
            _isActive = isActive;
        }
        #region ICommand<Vendor> Members

        public IResult<C.Vendor> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var data = (from x in ent.Vendors
                        where x.VendorId == _vendorId
                        select x).FirstOrDefault();

            data.IsActive = _isActive;
            dbContext.SaveChanges();

            var result = new Result<C.Vendor>(Status.Success, data.ToContract());
            return result;
        }

        #endregion

    }
}
