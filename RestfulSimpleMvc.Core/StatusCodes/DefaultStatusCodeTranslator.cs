using System.Net;

namespace RestfulSimpleMvc.Core.StatusCodes {
	public class DefaultStatusCodeTranslator : IStatusCodeTranslator {
		public HttpStatusCode LookUp(HttpStatusCode statusCode) {
			return statusCode;
		} 
	}
}