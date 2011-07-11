using System.Web.Mvc;
using Playground.Mvc.ResponseWriters;

namespace Playground.Mvc
{
	public class RestfulResult:ActionResult
	{
		private readonly IResponseWriter _responseWriter;
		private readonly object _content;

		public RestfulResult(IResponseWriter responseWriter, object content) {
			_responseWriter = responseWriter;
			_content = content;
		}

		public override void ExecuteResult(ControllerContext context) {
			_responseWriter.WriteResponse(context.HttpContext.Response, _content);
		}
	}
}