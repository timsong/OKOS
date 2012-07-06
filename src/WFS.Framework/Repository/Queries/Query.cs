using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace WFS.Repository
{
    public class Query<TEntity> : IQuery<TEntity>
        where TEntity : class
    {
        private Expression<Func<TEntity, bool>> _query;

        public Query(Expression<Func<TEntity, bool>> query)
        {
            this._query = query;
        }

        #region IQuery Members

        public IResult<TEntity> Execute(DbContext dbContext)
        {
            var result = new Result<TEntity>();
            var entities = dbContext.Set<TEntity>().Where(this._query).ToList();

            if (entities.Count == 0)
            {
                var information = new Message()
                {
                    Code = "ENTITY NOT FOUND",
                    Text = string.Format("No {0}s were found with the following query: ->{1}<-", typeof(TEntity).Name, this._query)
                };

                result.Status = Status.Error;
                result.Messages.Add(information);
                result.Value = default(TEntity);

                return result;
            }

            if (entities.Count > 1)
            {
                var information = new Message()
                {
                    Code = "MULTIPLE ENTITIES",
                    Text = string.Format("Multiple {0}s were found with the following query: ->{1}<-", typeof(TEntity).Name, this._query)
                };

                result.Status = Status.Success;
                result.Messages.Add(information);
                result.Value = entities.First();
                return result;
            }

            result.Status = Status.Success;
            result.Value = entities.First();

            return result;
        }

        #endregion
    }
}
