using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;


namespace WFS.Repository.Queries
{
    public class GetVendorListQuery : IListQuery<C.Vendor>
    {
        public IListResult<C.Vendor> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var data = ent.Vendors.AsEnumerable().Select(x => x.ToContract());

            var result = new ListResult<C.Vendor>(data.ToList());
            result.Status = Status.Success;
            return result;
        }

    }
}
