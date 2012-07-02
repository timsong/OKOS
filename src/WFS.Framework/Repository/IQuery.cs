using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFS.Repository
{
    public interface IQuery<TResult>
    {
        IResult<TResult> Execute();//DataContext dataContext);
    }
}
