using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.StatusCodes;

namespace RestfulSimpleMvc.Core.Results
{
	public interface IRestfulResultFactory {
		RestfulResult Build(IResponseWriter responseWriter, object content, string viewName, IStatusCodeWriter statusCodeWriter);
	}
}