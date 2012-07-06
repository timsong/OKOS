using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace WFS.Repository
{
    public class CountQuery<TEntity> : ICountQuery
        where TEntity : class
    {
        private Expression<Func<TEntity, bool>> _query;

        public CountQuery() { }
        public CountQuery(Expression<Func<TEntity, bool>> query)
        {
            this._query = query;
        }

        public int Execute(DbContext dbContext)
        {
            if (this._query != null)
            {
                return dbContext.Set<TEntity>().Count(this._query);
            }

            return dbContext.Set<TEntity>().Count();
        }
    }
}
