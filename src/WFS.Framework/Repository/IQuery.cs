using System.Data.Entity;

namespace WFS.Repository
{
    public interface IQuery<TResult>
    {
        IResult<TResult> Execute(DbContext dbContext);
    }
}
