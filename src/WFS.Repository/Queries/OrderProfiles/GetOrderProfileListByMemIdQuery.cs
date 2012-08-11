using System;
using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;


namespace WFS.Repository.Queries.OrderProfiles
{
    public class GetOrderProfileListByMemIdQuery : IListQuery<C.OrderProfile>
    {
        private readonly Guid _membershipId;

        public GetOrderProfileListByMemIdQuery(Guid membershipId)
        {
            _membershipId = membershipId;
        }

        IListResult<C.OrderProfile> IListQuery<C.OrderProfile>.Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;

            var data = from prof in ent.UserOrderProfiles
                       where prof.WFSUser.MembershipGuid == _membershipId && prof.IsActive
                       select prof;

            var result = new ListResult<C.OrderProfile>(data.Select(x => x.ToContract()).ToList());
            result.Status = Status.Success;
            return result;
        }
    }
}
