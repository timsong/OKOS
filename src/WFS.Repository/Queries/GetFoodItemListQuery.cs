using System.Linq;
using WFS.Contract.Enums;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Queries
{
    public class GetFoodItemListQuery : IListQuery<C.FoodItem>
    {
        private readonly int _vendorId;
        private readonly ActiveDataRequestEnum _activeDataRequest;

        public GetFoodItemListQuery(int vendorId, ActiveDataRequestEnum activeDataRequest)
        {
            _vendorId = vendorId;
            _activeDataRequest = activeDataRequest;
        }

        IListResult<C.FoodItem> IListQuery<C.FoodItem>.Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;

            var data = from fi in ent.FoodItems
                       join fc in ent.FoodCategories on fi.FoodCategoryID equals fc.FoodCategoryId
                       where fc.VendorID == _vendorId
                       select fi;

            if (_activeDataRequest == ActiveDataRequestEnum.ActiveOnly)
                data = data.Where(x => x.IsActive);

            var result = new ListResult<C.FoodItem>(data.Select(x => x.ToContract()).ToList());
            result.Status = Status.Success;
            return result;
        }
    }
}
