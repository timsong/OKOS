using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFS.Repository
{
    public interface IRepository
    {
        bool ExecuteExistsQuery(IExistsQuery query);
        int ExecuteCountQuery(ICountQuery query);
        IResult<TResult> ExecuteCommand<TResult>(ICommand<TResult> command);
        IResult<TResult> ExecuteQuery<TResult>(IQuery<TResult> query);
        IListResult<TResult> ExecuteQuery<TResult>(IListQuery<TResult> query);
    }
}
