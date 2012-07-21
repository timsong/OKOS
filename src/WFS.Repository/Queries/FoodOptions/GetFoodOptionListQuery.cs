using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Queries
{
    public class GetFoodCategoryListQuery : IListQuery<C.FoodCategory>
    {
        private readonly int _vendorID;
        public GetFoodCategoryListQuery(int vendorId)
        {
            _vendorID = vendorId;
        }

        public IListResult<C.FoodCategory> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var data = ent.VendorFoodCategories.Where(x => x.OrganizationID == _vendorID).AsEnumerable().Select(x => x.ToContract());

            var result = new ListResult<C.FoodCategory>(data.ToList());
            result.Status = Status.Success;
            return result;
        }

    }
}
