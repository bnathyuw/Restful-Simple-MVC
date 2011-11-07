using System.Net;

namespace RestfulSimpleMvc.Core.StatusCodes {
	public class HtmlStatusCodeTranslator : IStatusCodeTranslator{
		public HttpStatusCode LookUp(HttpStatusCode statusCode) {
			switch(statusCode) {
				case HttpStatusCode.Created:
					return HttpStatusCode.MovedPermanently;
				default:
					return statusCode;
			}
		}
	}
}