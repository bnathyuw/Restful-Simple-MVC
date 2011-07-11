using Playground.Mvc.ResponseWriters;

namespace Playground.Mvc.Results
{
	public interface IRestfulResultFactory {
		RestfulResult Build(IResponseWriter responseWriter, object content);
	}
}