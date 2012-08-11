using System;
using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Queries
{
    public class GetWFSUserInfoByMembershipIdQuery : IQuery<C.WFSUser>
    {
        private readonly Guid _memId;

        public GetWFSUserInfoByMembershipIdQuery(Guid membershipId)
        {
            _memId = membershipId;
        }

        #region IQuery<C.WFSUser> Members

        public IResult<C.WFSUser> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var data = (from x in ent.WFSUsers
                        join y in ent.Users on x.MembershipGuid equals y.UserId
                        where y.UserId == _memId
                        select x).FirstOrDefault();

            if (data != null)
                return new Result<C.WFSUser>(Status.Success, data.ToContract());
            else
                return new Result<C.WFSUser>(Status.Error);
        }

        #endregion
    }
}
