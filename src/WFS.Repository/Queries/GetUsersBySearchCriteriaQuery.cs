using System;
using System.Collections.Generic;
using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Queries
{
    public class GetUsersBySearchCriteriaQuery : IListQuery<C.WFSUser>
    {
        private readonly string _searchText;
        private readonly string _roleFilter;

        public GetUsersBySearchCriteriaQuery(string searchText, string roleFilter)
        {
            _searchText = searchText;
            _roleFilter = roleFilter;
        }

        IListResult<C.WFSUser> IListQuery<C.WFSUser>.Execute(System.Data.Entity.DbContext dbContext)
        {

            var db = (WFS.DataContext.WFSEntities)dbContext;
            var list = new List<C.WFSUser>();

            var users = (from x in db.WFSUsers
                         join y in db.Memberships on x.MembershipGuid equals y.UserId
                         where x.FirstName.Contains(_searchText) || x.LastName.Contains(_searchText) || y.Email.Contains(_searchText)
                         orderby x.LastName, x.FirstName
                         select x).AsEnumerable();

            if (!String.IsNullOrEmpty(_roleFilter))
            {
                foreach (var x in users)
                    if (x.User.Roles.Select(r => r.RoleName).Contains(_roleFilter))
                        list.Add(x.ToContract());
            }
            else
                list = users.Select(x => x.ToContract()).ToList();

            var result = new ListResult<C.WFSUser>(list);
            result.Status = Status.Success;
            return result;
        }
    }
}
