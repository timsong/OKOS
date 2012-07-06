using System;
using WFS.Contract;

namespace WFS.Repository.Queries
{
    public class GetVendorListQuery : IListQuery<Vendor>
    {
        #region IListQuery<Vendor> Members

        public IListResult<Vendor> Execute()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
