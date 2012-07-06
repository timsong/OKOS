using System.Linq;

namespace WFS.Repository
{
    public interface IQueryFilter<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Filter(IQueryable<TEntity> query);
    }
}
