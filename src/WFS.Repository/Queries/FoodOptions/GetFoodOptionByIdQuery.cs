using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;
using System.Collections.Generic;

namespace WFS.Repository.Queries
{
    public class GetFoodOptionByIdQuery : IQuery<C.FoodOption>
    {
        private readonly int _foodOptionId;

		public GetFoodOptionByIdQuery (int foodOptionId)
        {
			_foodOptionId = foodOptionId;
        }

        public IResult<C.FoodOption> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;

            var data = ent.VendorFoodOptions.FirstOrDefault(x => x.VendorFoodOptionId == _foodOptionId).ToContract();

            var result = new Result<C.FoodOption>();

			result.Value = data;

			result.Status = Status.Success;
            
			return result;
        }

    }
}
