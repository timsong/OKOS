using System.Linq;
using WFS.Contract.Enums;
using WFS.Repository.Conversions;
using C = WFS.Contract;


namespace WFS.Repository.Queries
{
    public class GetOrganizationsByTypeListQuery : IListQuery<C.Organization>
    {
        private readonly OrganizationTypeEnum _type;
        private readonly ActiveDataRequestEnum _dataRequest;

        public GetOrganizationsByTypeListQuery(OrganizationTypeEnum type, ActiveDataRequestEnum dataRequest)
        {
            _type = type;
            _dataRequest = dataRequest;
        }

        public IListResult<C.Organization> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var type = _type.ToString();

            var temp = ent.Organizations.Where(x => x.OrganizationType == type && !x.IsDeleted);

            if (_dataRequest == ActiveDataRequestEnum.ActiveOnly)
                temp = temp.Where(x => x.IsActive);

            var data = temp.OrderBy(x => x.Name).AsEnumerable().Select(x => x.ToContract());
            var result = new ListResult<C.Organization>(data.ToList());
            result.Status = Status.Success;
            return result;
        }

    }
}
