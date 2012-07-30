using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFS.Framework
{
	public abstract class EditModelBase<T> : IResponse
	{
		public EditModelBase()
		{
			Messages = new List<Message>();
		}

		public List<Message> Messages { get; set; }

		public Status Status { get; set; }
	}
}
