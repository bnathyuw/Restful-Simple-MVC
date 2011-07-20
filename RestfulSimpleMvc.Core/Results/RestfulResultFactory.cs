using RestfulSimpleMvc.Core.ResponseWriters;

namespace RestfulSimpleMvc.Core.Results
{
	public class RestfulResultFactory : IRestfulResultFactory
	{
		public RestfulResult Build(IResponseWriter responseWriter, object content, string viewName) {
			return new RestfulResult(responseWriter, content, viewName);
		}
	}
}