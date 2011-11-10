using System;
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
		private readonly IContextHelper _contextHelper;

		public RestfulResult(IResponseWriter responseWriter, object content, string viewName, IResponseUpdater responseUpdater, IStatusCodeTranslator statusCodeTranslator, ILocationProvider locationProvider, IContextHelper contextHelper) {
			_responseWriter = responseWriter;
			_responseUpdater = responseUpdater;
			_statusCodeTranslator = statusCodeTranslator;
			_locationProvider = locationProvider;
			_contextHelper = contextHelper;
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
			else if (_viewName == "PUT") {
				var newLocation = _locationProvider.GetLocation(_content, context);
				var currentLocation = _contextHelper.GetRequestLocation(context);
				if (!newLocation.Equals(currentLocation, StringComparison.InvariantCultureIgnoreCase)) {
					var statusCode = _statusCodeTranslator.LookUp(HttpStatusCode.MovedPermanently);
					_responseUpdater.SetStatusCode(context, statusCode);
					_responseUpdater.SetLocation(context, newLocation);
				}
			}
			else {
				
				if (_content is IStatusCoded) {
					_responseUpdater.SetStatusCode(context, ((IStatusCoded)_content).HttpStatusCode);
					_responseWriter.WriteResponse(context, _content, _viewName);
				}
				else if (_content == null) {
					var statusCode = _statusCodeTranslator.LookUp(HttpStatusCode.NoContent);
					_responseUpdater.SetStatusCode(context, statusCode);
				}
				else {
					_responseWriter.WriteResponse(context, _content, _viewName);
				}


			}
		}
	}

	public interface IContextHelper {
		string GetRequestLocation(ControllerContext context);
	}

	public class ContextHelper:IContextHelper{
		public string GetRequestLocation(ControllerContext context) {
			return context.HttpContext.Request.Path;
		}
	}
}