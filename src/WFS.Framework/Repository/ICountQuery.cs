using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace WFS.Repository
{
    public interface ICountQuery
    {
        int Execute(DbContext dbContext);
    }
}
