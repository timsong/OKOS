using System.Collections.Generic;

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

        public Result(Status status, TValue value)
            : this()
        {
            this.Status = status;
            this.Value = value;
        }

        public Status Status { get; set; }
        public List<Message> Messages { get; set; }
        public TValue Value { get; set; }
    }
}
