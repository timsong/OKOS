using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFS.Repository
{
    public interface IResult
    {
        Status Status { get; set; }
        IList<Message> Messages { get; set; }
    }

    public interface IResult<TValue> : IResult
    {
        TValue Value { get; set; }
    }
}
