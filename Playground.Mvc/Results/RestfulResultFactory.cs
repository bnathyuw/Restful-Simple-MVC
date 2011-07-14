using Playground.Mvc.ResponseWriters;

namespace Playground.Mvc.Results
{
	public class RestfulResultFactory : IRestfulResultFactory
	{
		public RestfulResult Build(IResponseWriter responseWriter, object content, string viewName) {
			return new RestfulResult(responseWriter, content, viewName);
		}
	}
}