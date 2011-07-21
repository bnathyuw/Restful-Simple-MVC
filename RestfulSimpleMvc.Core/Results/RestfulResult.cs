using System;
using System.Web.Mvc;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.StatusCodes;

namespace RestfulSimpleMvc.Core.Results
{
	public class RestfulResult:ActionResult
	{
		private readonly IResponseWriter _responseWriter;
		private readonly object _content;
		private readonly string _viewName;
		private readonly IStatusCodeWriter _statusCodeWriter;

		public RestfulResult(IResponseWriter responseWriter, object content, string viewName, IStatusCodeWriter statusCodeWriter) {
			_responseWriter = responseWriter;
			_statusCodeWriter = statusCodeWriter;
			_viewName = viewName;
			_content = content;
		}

		public override void ExecuteResult(ControllerContext context) {
			_responseWriter.WriteResponse(context, _content, _viewName);
			_statusCodeWriter.WriteStatusCode(context, _content);
		}
	}
}