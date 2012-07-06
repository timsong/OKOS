using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFS.Framework.Responses
{
    public class BaseListResponse : BaseResponse, IListResponse
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}
