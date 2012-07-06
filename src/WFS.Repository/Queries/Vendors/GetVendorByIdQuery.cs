using System;
using WFS.Contract;

namespace WFS.Repository.Queries.Vendors
{
    public class GetVendorByIdQuery : IQuery<Vendor>
    {
        public int _vendorId { get; set; }

        public GetVendorByIdQuery(int vendorId)
        {
            _vendorId = vendorId;
        }

        #region IQuery<Vendor> Members

        public IResult<Vendor> Execute(System.Data.Entity.DbContext dbContext)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
