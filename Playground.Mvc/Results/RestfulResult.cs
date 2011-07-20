using System;
using System.Web.Mvc;
using RestfulSimpleMvc.Core.Exceptions;
using RestfulSimpleMvc.Core.ResponseWriters;

namespace RestfulSimpleMvc.Core.Results
{
	public class RestfulResult:ActionResult
	{
		private readonly IResponseWriter _responseWriter;
		private readonly object _content;
		private readonly string _viewName;

		public RestfulResult(IResponseWriter responseWriter, object content, string viewName) {
			_responseWriter = responseWriter;
			_viewName = viewName;
			_content = content;
		}

		public override void ExecuteResult(ControllerContext context) {
			_responseWriter.WriteResponse(context, _content, _viewName);
			
			var exceptionContent = _content as RestfulException;
			if (exceptionContent != null) {
				context.HttpContext.Response.StatusCode = (Int32) exceptionContent.HttpStatusCode;
			}
		}
	}
}