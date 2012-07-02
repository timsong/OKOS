using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFS
{
    public class Message
    {
        public Message()
        {
            this.Code = string.Empty;
            this.Text = string.Empty;
        }

        public Message(string code, string message)
            : this()
        {
            this.Code = code;
            this.Text = message;
        }

        public string Text { get; set; }
        public string Code { get; set; }
    }
}
