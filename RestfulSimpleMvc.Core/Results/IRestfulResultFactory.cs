using RestfulSimpleMvc.Core.Location;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.StatusCodes;

namespace RestfulSimpleMvc.Core.Results
{
	public interface IRestfulResultFactory {
		RestfulResult Build(IResponseWriter responseWriter, object content, string viewName, IStatusCodeTranslator statusCodeTranslator, ILocationProvider locationProviderFactory);
	}
}