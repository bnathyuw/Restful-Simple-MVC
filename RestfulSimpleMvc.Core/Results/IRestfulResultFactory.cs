using RestfulSimpleMvc.Core.ResponseWriters;

namespace RestfulSimpleMvc.Core.Results
{
	public interface IRestfulResultFactory {
		RestfulResult Build(IResponseWriter responseWriter, object content, string viewName, IResponseUpdater responseUpdater);
	}
}