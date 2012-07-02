using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFS.Repository
{
    public class Result<TValue> : IResult<TValue>
    {
        public Result()
        {
            this.Messages = new List<Message>();
            this.Status = Status.Success;
        }

        public Result(Status status)
            : this()
        {
            this.Status = status;
        }

        public Status Status { get; set; }
        public IList<Message> Messages { get; set; }
        public TValue Value { get; set; }
    }
}
