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
		private readonly IResponseUpdater _responseUpdater;

		public RestfulResult(IResponseWriter responseWriter, object content, string viewName, IResponseUpdater responseUpdater) {
			_responseWriter = responseWriter;
			_responseUpdater = responseUpdater;
			_viewName = viewName;
			_content = content;
		}

		public override void ExecuteResult(ControllerContext context) {
			if (_viewName == "POST") {
				_responseWriter.WriteCreated(context, _content);
			}
			else {
				_responseWriter.WriteResponse(context, _content, _viewName);
				if (_content is IStatusCoded) {
					_responseUpdater.SetStatusCode(context, ((IStatusCoded) _content).HttpStatusCode);
				}
			}
		}
	}
}