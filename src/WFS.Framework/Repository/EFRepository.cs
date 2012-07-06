using System.Data.Entity;

namespace WFS.Repository
{
    public class EFRepository : IRepository
    {
        private DbContext _dbContext;

        public EFRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public bool ExecuteExistsQuery(IExistsQuery query)
        {
            return query.Execute(this._dbContext);
        }

        public int ExecuteCountQuery(ICountQuery query)
        {
            return query.Execute(this._dbContext);
        }

        public IResult<TResult> ExecuteCommand<TResult>(ICommand<TResult> command)
        {
            return command.Execute(this._dbContext);
        }

        public IResult<TResult> ExecuteQuery<TResult>(IQuery<TResult> query)
        {
            return query.Execute(this._dbContext);
        }

        public IListResult<TResult> ExecuteQuery<TResult>(IListQuery<TResult> query)
        {
            return query.Execute(this._dbContext);
        }
    }
}
