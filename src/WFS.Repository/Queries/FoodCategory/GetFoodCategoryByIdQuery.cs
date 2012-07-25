using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Queries
{
    public class GetFoodCategoryByIdQuery : IQuery<C.FoodCategory>
    {
        public int _foodCategoryId { get; set; }

        public GetFoodCategoryByIdQuery(int foodCategoryId)
        {
            _foodCategoryId = foodCategoryId;
        }

        #region IQuery<Vendor> Members

        public IResult<C.FoodCategory> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var data = (from x in ent.VendorFoodCategories
                        where x.VendorFoodCategoryId == _foodCategoryId
                        select x).FirstOrDefault().ToContract();

            var result = new Result<C.FoodCategory>(Status.Success, data);
            return result;
        }

        #endregion
    }
}
