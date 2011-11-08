using RestfulSimpleMvc.Core.Location;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.StatusCodes;

namespace RestfulSimpleMvc.Core.Results
{
	public class RestfulResultFactory : IRestfulResultFactory
	{
		public RestfulResult Build(IResponseWriter responseWriter, object content, string viewName, IResponseUpdater responseUpdater, IStatusCodeTranslator statusCodeTranslator, ILocationProvider locationProvider) {
			return new RestfulResult(responseWriter, content, viewName, responseUpdater, statusCodeTranslator, locationProvider);
		}
	}
}