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

        public ListResult(IList<TValue> values)
            : this()
        {
            this.Values = values;
            this.Total = this.Values.Count;
        }

        public Status Status { get; set; }
        public IList<Message> Messages { get; set; }

        public IList<TValue> Values { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}
