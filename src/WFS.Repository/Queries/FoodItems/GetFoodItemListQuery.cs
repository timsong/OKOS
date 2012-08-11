using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;
using System.Collections.Generic;

namespace WFS.Repository.Queries
{
	public class GetFoodItemsByVendorQuery : IQuery<List<C.FoodItem>>
	{
		private readonly int _vendorID;

		public GetFoodItemsByVendorQuery(int vendorId)
		{
			_vendorID = vendorId;
		}

		public IResult<List<C.FoodItem>> Execute(System.Data.Entity.DbContext dbContext)
		{
			var ent = (WFS.DataContext.WFSEntities)dbContext;

			var data = ent.FoodItems.Where(x => x.VendorFoodCategory.OrganizationID == _vendorID).AsEnumerable().Select(x => x.ToContract());

			var result = new Result<List<C.FoodItem>>();

			result.Value = data.ToList();

			result.Status = Status.Success;

			return result;
		}

	}
}
