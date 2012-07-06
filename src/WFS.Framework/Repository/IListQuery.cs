using System.Data.Entity;

namespace WFS.Repository
{
    public interface IListQuery<TResult>
    {
        IListResult<TResult> Execute(DbContext dbContext);
    }
}
