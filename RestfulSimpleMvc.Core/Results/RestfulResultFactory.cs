using RestfulSimpleMvc.Core.ResponseWriters;

namespace RestfulSimpleMvc.Core.Results
{
	public class RestfulResultFactory : IRestfulResultFactory
	{
		public RestfulResult Build(IResponseWriter responseWriter, object content, string viewName, IResponseUpdater responseUpdater) {
			return new RestfulResult(responseWriter, content, viewName, responseUpdater);
		}
	}
}