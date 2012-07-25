using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Queries
{
    public class GetFoodCategoryByIdQuery : IQuery<C.FoodCategory>
    {
        public int _vendorId { get; set; }

        public GetFoodCategoryByIdQuery(int vendorId)
        {
            _vendorId = vendorId;
        }

        #region IQuery<Vendor> Members

        public IResult<C.FoodCategory> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var data = (from x in ent.VendorFoodCategories
                        where x.OrganizationID == _vendorId
                        select x).FirstOrDefault().ToContract();

            var result = new Result<C.FoodCategory>(Status.Success, data);
            return result;
        }

        #endregion
    }
}
