using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFS.Framework
{
    public interface IResponse
    {
        Status Status { get; set; }
        List<Message> Messages { get; set; }
    }
}
