using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFS.Repository
{
    public interface IOrderBy<TEntity> where TEntity : class
    {
        void ApplyOrderBy(ref IQueryable<TEntity> query);
    }
}
