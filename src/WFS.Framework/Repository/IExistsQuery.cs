using System.Data.Entity;

namespace WFS.Repository
{
    public interface IExistsQuery
    {
        bool Execute(DbContext dbContext);
    }
}
