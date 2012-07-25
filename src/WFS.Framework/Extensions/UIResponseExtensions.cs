using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Framework.Responses;
using WFS.Repository;

namespace WFS.Framework.Extensions
{
	public static class UIResponseExtensions
	{
		public static UIResponse<SUBJECTTYPE> ToUIResult<SUBJECTTYPE, ORIGINALTYPE>(this Result<ORIGINALTYPE> response
			, Func<ORIGINALTYPE, SUBJECTTYPE> converterF)
		{
			return response.ToUIResult<SUBJECTTYPE, ORIGINALTYPE>(converterF, (o) => string.Empty);
		}

		public static UIResponse<SUBJECTTYPE> ToUIResult<SUBJECTTYPE, ORIGINALTYPE>(this Result<ORIGINALTYPE> response
			, Func<ORIGINALTYPE, SUBJECTTYPE> converterF
			, Func<SUBJECTTYPE, string> htmlBuilderF)
		{
			UIResponse<SUBJECTTYPE> uiresponse = new UIResponse<SUBJECTTYPE>();

			uiresponse.Messages = response.Messages;

			uiresponse.Status = response.Status;

			uiresponse.Subject = converterF(response.Value);

			uiresponse.HtmlResult = htmlBuilderF(uiresponse.Subject);

			return uiresponse;
		}

		public static UIResponse<SUBJECTTYPE> ToUIResult<SUBJECTTYPE>(this BaseResponse response
		, Func<SUBJECTTYPE> converterF)
		{
			return response.ToUIResult<SUBJECTTYPE>(converterF, (o) => string.Empty);
		}

		public static UIResponse<SUBJECTTYPE> ToUIResult<SUBJECTTYPE>(this BaseResponse response
			, Func<SUBJECTTYPE> converterF
			, Func<SUBJECTTYPE, string> htmlBuilderF)
		{
			UIResponse<SUBJECTTYPE> uiresponse = new UIResponse<SUBJECTTYPE>();

			uiresponse.Messages = response.Messages;

			uiresponse.Status = response.Status;

			uiresponse.Subject = converterF();

			uiresponse.HtmlResult = htmlBuilderF(uiresponse.Subject);

			return uiresponse;
		}
	}
}
