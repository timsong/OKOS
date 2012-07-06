using System.Collections.Generic;

namespace WFS.Repository
{
    public class ListResult<TValue> : IListResult<TValue>
    {
        public ListResult()
        {
            this.Messages = new List<Message>();
            this.Values = new List<TValue>();
        }

        public ListResult(Status status)
            : this()
        {
            this.Status = status;
        }

        public ListResult(List<TValue> values)
            : this()
        {
            this.Values = values;
            this.Total = this.Values.Count;
        }

        public Status Status { get; set; }
        public List<Message> Messages { get; set; }

        public List<TValue> Values { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}
