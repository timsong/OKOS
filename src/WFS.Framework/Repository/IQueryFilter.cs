using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFS.Repository
{
    public interface IQueryFilter<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Filter(IQueryable<TEntity> query);
    }
}
