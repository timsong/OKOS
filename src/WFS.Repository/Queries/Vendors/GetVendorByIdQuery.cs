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

        public IResult<Vendor> Execute()
        {
            throw new NotImplementedException();
        }
    }
}
