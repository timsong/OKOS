using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFS.Repository
{
    public interface IListResult : IResult, IPageable
    {
        int Total { get; set; }
    }

    public interface IListResult<TValue> : IListResult
    {
        IList<TValue> Values { get; set; }
    }
}
