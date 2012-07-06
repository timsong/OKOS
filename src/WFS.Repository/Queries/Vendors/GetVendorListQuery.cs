using C = WFS.Contract;
using System;

namespace WFS.Repository.Queries
{
    public class GetVendorListQuery : IListQuery<C.Vendor>
    {

        #region IListQuery<Vendor> Members

        public IListResult<C.Vendor> Execute(System.Data.Entity.DbContext dbContext)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
