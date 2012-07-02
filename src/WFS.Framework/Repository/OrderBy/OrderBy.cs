using System;
using System.Linq;
using System.Linq.Expressions;

namespace WFS.Repository
{
    public class OrderBy<TEntity, TProperty> : IOrderBy<TEntity> where TEntity : class
    {
        private Expression<Func<TEntity, TProperty>> _expression;
        private SortDirection _sortDirection = SortDirection.Ascending;

        public OrderBy(Expression<Func<TEntity, TProperty>> expression)
        {
            this._expression = expression;
        }

        public OrderBy(Expression<Func<TEntity, TProperty>> expression, SortDirection sortDirection)
            : this(expression)
        {
            this._sortDirection = sortDirection;
        }

        public void ApplyOrderBy(ref IQueryable<TEntity> query)
        {
            if (this._sortDirection == SortDirection.Ascending)
            {
                query = query.OrderBy(this._expression);
            }
            else
            {
                query = query.OrderByDescending(this._expression);
            }
        }
    }
}
