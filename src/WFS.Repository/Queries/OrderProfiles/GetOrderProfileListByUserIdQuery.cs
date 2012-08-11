using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;


namespace WFS.Repository.Queries
{
    public class GetOrderProfileListByUserIdQuery : IListQuery<C.OrderProfile>
    {
        private readonly int _userId;

        public GetOrderProfileListByUserIdQuery(int userId)
        {
            _userId = userId;
        }

        IListResult<C.OrderProfile> IListQuery<C.OrderProfile>.Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;

            var data = from prof in ent.UserOrderProfiles
                       where prof.UserId == _userId && prof.IsActive
                       orderby prof.LastName, prof.FirstName
                       select prof;

            var result = new ListResult<C.OrderProfile>(data.AsEnumerable().Select(x => x.ToContract()).ToList());
            result.Status = Status.Success;
            return result;
        }
    }
}
