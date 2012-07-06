using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace WFS.Repository
{
    public class ExistsQuery<TEntity> : IExistsQuery 
        where TEntity : class
    {
        private Expression<Func<TEntity, bool>> _query;

        public ExistsQuery() { }
        public ExistsQuery(Expression<Func<TEntity, bool>> query)
        {
            this._query = query;
        }

        public bool Execute(DbContext dataContext)
        {
            if (this._query != null)
            {
                return dataContext.Set<TEntity>().Any(this._query);
            }

            return dataContext.Set<TEntity>().Any();
        }
    }
}
