using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Queries
{
    public class GetVendorByIdQuery : IQuery<C.Vendor>
    {
        public int _vendorId { get; set; }

        public GetVendorByIdQuery(int vendorId)
        {
            _vendorId = vendorId;
        }

        #region IQuery<Vendor> Members

        public IResult<C.Vendor> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var data = (from x in ent.Organizations
                        where x.OrganizationId == _vendorId
                        select x).FirstOrDefault().ToContract();

            var result = new Result<C.Vendor>(Status.Success, data);
            return result;
        }

        #endregion
    }
}
