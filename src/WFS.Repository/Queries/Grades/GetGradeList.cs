using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Queries.Grades
{
    public class GetGradeList : IListQuery<C.Grade>
    {
        public GetGradeList()
        {
        }

        public IListResult<C.Grade> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;

            var data = ent.Grades.OrderBy(x => x.Order).AsEnumerable().Select(x => x.ToContract());

            var result = new ListResult<C.Grade>();
            result.Values = data.ToList();
            result.Status = Status.Success;

            return result;
        }
    }
}
