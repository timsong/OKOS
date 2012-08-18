using System;
using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Queries
{
    public class GetWFSUserInfoByIdQuery : IQuery<C.WFSUser>
    {
        private readonly Guid? _memId;
        private readonly int? _userId;

        public GetWFSUserInfoByIdQuery(Guid? membershipId, int? userId)
        {
            _memId = membershipId;
            _userId = userId;
        }

        #region IQuery<C.WFSUser> Members

        public IResult<C.WFSUser> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var data = (from x in ent.WFSUsers
                        join y in ent.Users on x.MembershipGuid equals y.UserId
                        where (_memId.HasValue && y.UserId == _memId) || (_userId.HasValue && x.UserId == _userId)
                        select x).FirstOrDefault();

            if (data != null)
                return new Result<C.WFSUser>(Status.Success, data.ToContract());
            else
                return new Result<C.WFSUser>(Status.Error);
        }

        #endregion
    }
}
