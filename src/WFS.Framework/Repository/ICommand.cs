using System.Data.Entity;

namespace WFS.Repository
{
    public interface ICommand<TResult>
    {
        IResult<TResult> Execute(DbContext dbContext);
    }
}
