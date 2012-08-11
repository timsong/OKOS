using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;
using WFS.Contract.Enums;


namespace WFS.Repository.Queries
{
    public class GetOrganizationsByTypeListQuery : IListQuery<C.Organization>
    {
        private readonly OrganizationTypeEnum _type;

        public GetOrganizationsByTypeListQuery(OrganizationTypeEnum type)
        { 
            _type = type;
        }

        public IListResult<C.Organization> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var type = _type.ToString();

            var data = ent.Organizations.Where(x => x.OrganizationType == type && !x.IsDeleted).AsEnumerable().Select(x => x.ToContract());

            var result = new ListResult<C.Organization>(data.ToList());
            result.Status = Status.Success;
            return result;
        }

    }
}
