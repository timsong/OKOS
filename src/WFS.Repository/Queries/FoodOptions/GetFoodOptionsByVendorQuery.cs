using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;
using System.Collections.Generic;

namespace WFS.Repository.Queries
{
    public class GetFoodOptionsByVendorQuery : IQuery<List<C.FoodOption>>
    {
        private readonly int _vendorID;

		public GetFoodOptionsByVendorQuery(int vendorId)
        {
            _vendorID = vendorId;
        }

        public IResult<List<C.FoodOption>> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            
			var data = ent.VendorFoodOptions.Where(x => x.OrganizationId == _vendorID).AsEnumerable().Select(x => x.ToContract());

            var result = new Result<List<C.FoodOption>>();

			result.Value = data.ToList();

            result.Status = Status.Success;

			return result;
        }

    }
}
