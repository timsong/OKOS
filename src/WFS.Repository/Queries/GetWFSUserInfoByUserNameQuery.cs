using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;


namespace WFS.Repository.Queries
{
    public class GetWFSUserInfoByUserNameQuery : IQuery<C.WFSUser>
    {
        private readonly string _userName;

        public GetWFSUserInfoByUserNameQuery(string userName)
        {
            _userName = userName;
        }

        #region IQuery<C.WFSUser> Members

        public IResult<C.WFSUser> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var data = (from x in ent.WFSUsers
                        join y in ent.Users on x.MembershipGuid equals y.UserId
                        where y.UserName == _userName
                        select x).FirstOrDefault();

            if (data != null)
                return new Result<C.WFSUser>(Status.Success, data.ToContract());
            else
                return new Result<C.WFSUser>(Status.Error);
        }

        #endregion
    }
}
