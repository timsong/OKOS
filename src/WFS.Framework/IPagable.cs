using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFS
{
    public interface IPageable
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
    }
}
