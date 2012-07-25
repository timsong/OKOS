using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Repository;

namespace WFS.Framework.Responses
{
	public class UIResponse<SUBJECTTYPE> : BaseResponse
	{
		public SUBJECTTYPE Subject { get; set; }

		public string HtmlResult { get; set; }

		public object AdditionalPayload { get; set; }
	}
}
