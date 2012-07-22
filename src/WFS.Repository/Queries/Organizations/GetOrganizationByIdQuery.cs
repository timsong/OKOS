using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Queries
{
    public class GetOrganizationByIdQuery : IQuery<C.Organization>
    {
        public int _organizationId { get; set; }

        public GetOrganizationByIdQuery(int organizationId)
        {
            _organizationId = organizationId;
        }

        #region IQuery<Vendor> Members

        public IResult<C.Organization> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var data = (from x in ent.Organizations
                        where x.OrganizationId == _organizationId
                        select x).FirstOrDefault().ToContract();

            var result = new Result<C.Organization>(Status.Success, data);
            return result;
        }

        #endregion
    }
}
