using Playground.Mvc.ResponseWriters;
using Playground.Mvc.Results;

namespace Playground.Mvc
{
	public class RestfulResultFactory : IRestfulResultFactory
	{
		public RestfulResult Build(IResponseWriter responseWriter, object content) {
			return new RestfulResult(responseWriter, content);
		}
	}
}