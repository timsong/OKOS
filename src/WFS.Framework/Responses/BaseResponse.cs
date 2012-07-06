using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFS.Framework.Responses
{
    public class BaseResponse : IResponse
    {
        public BaseResponse()
        {
            this.Messages = new List<Message>();
            this.Status = Status.Success;
        }

        public Status Status { get; set; }

        public List<Message> Messages { get; set; }
    }
}
