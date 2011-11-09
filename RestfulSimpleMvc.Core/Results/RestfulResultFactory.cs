using RestfulSimpleMvc.Core.Location;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.StatusCodes;

namespace RestfulSimpleMvc.Core.Results
{
	public class RestfulResultFactory : IRestfulResultFactory
	{
		private readonly IResponseUpdater _responseUpdater;
		private readonly IContextHelper _contextHelper;

		public RestfulResultFactory(IResponseUpdater responseUpdater, IContextHelper contextHelper) {
			_responseUpdater = responseUpdater;
			_contextHelper = contextHelper;
		}

		public RestfulResult Build(IResponseWriter responseWriter, object content, string viewName, IStatusCodeTranslator statusCodeTranslator, ILocationProvider locationProvider) {
			return new RestfulResult(responseWriter, content, viewName, _responseUpdater, statusCodeTranslator, locationProvider, _contextHelper);
		}
	}
}