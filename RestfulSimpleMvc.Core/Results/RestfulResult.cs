using System.Net;
using System.Web.Mvc;
using RestfulSimpleMvc.Core.Location;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.StatusCodes;

namespace RestfulSimpleMvc.Core.Results
{
	public class RestfulResult:ActionResult
	{
		private readonly IResponseWriter _responseWriter;
		private readonly object _content;
		private readonly string _viewName;
		private readonly IResponseUpdater _responseUpdater;
		private readonly IStatusCodeTranslator _statusCodeTranslator;
		private readonly ILocationProvider _locationProvider;

		public RestfulResult(IResponseWriter responseWriter, object content, string viewName, IResponseUpdater responseUpdater, IStatusCodeTranslator statusCodeTranslator, ILocationProvider locationProvider) {
			_responseWriter = responseWriter;
			_responseUpdater = responseUpdater;
			_statusCodeTranslator = statusCodeTranslator;
			_locationProvider = locationProvider;
			_viewName = viewName;
			_content = content;
		}

		public override void ExecuteResult(ControllerContext context) {
			if (_viewName == "POST") {
				var statusCode = _statusCodeTranslator.LookUp(HttpStatusCode.Created);
				_responseUpdater.SetStatusCode(context, statusCode);
				var location = _locationProvider.GetLocation(_content, context);
				_responseUpdater.SetLocation(context, location);
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