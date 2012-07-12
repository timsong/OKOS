using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Queries
{
    public class GetFoodOptionListQuery : IListQuery<C.FoodOption>
    {
        private readonly int _vendorID;
        public GetFoodOptionListQuery(int vendorId)
        {
            _vendorID = vendorId;
        }

        public IListResult<C.FoodOption> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var data = ent.FoodOptions.Where(x => x.VendorId == _vendorID).AsEnumerable().Select(x => x.ToContract());

            var result = new ListResult<C.FoodOption>(data.ToList());
            result.Status = Status.Success;
            return result;
        }

    }
}
