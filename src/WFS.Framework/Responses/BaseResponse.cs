using System.Collections.Generic;

namespace WFS.Framework
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
