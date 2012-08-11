using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Queries
{
    public class GetFoodItemByIdQuery : IQuery<C.FoodItem>
    {
        public int _foodItemId { get; set; }

        public GetFoodItemByIdQuery(int foodItemId)
        {
            _foodItemId = foodItemId;
        }

        #region IQuery<Vendor> Members

        public IResult<C.FoodItem> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var data = (from x in ent.FoodItems
                        where x.FoodItemId == _foodItemId
                        select x).FirstOrDefault().ToContract();

            var result = new Result<C.FoodItem>(Status.Success, data);
            return result;
        }

        #endregion
    }
}
