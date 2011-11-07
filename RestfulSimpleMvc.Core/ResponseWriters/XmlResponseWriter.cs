using System.Net;
using System.Web.Mvc;

namespace RestfulSimpleMvc.Core.ResponseWriters
{
	public class XmlResponseWriter : IResponseWriter {
		private readonly IResponseUpdater _responseUpdater;
		private readonly ISerializationDataProviderFactory _serializationDataProviderFactory;

    	public XmlResponseWriter(IResponseUpdater responseUpdater, ISerializationDataProviderFactory serializationDataProviderFactory) {
    		_responseUpdater = responseUpdater;
    		_serializationDataProviderFactory = serializationDataProviderFactory;
    	}

		public void WriteResponse(ControllerContext controllerContext, object content, string viewName) {
			if (controllerContext.RouteData.Values["action"].ToString() == "POST") {
				controllerContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.Created;
				
				return;
			}
			var serializer = _serializationDataProviderFactory.Build(content);
			var xDocument = serializer.GetXmlData(content);
			_responseUpdater.WriteOutputToResponse(controllerContext, xDocument.ToString());
			_responseUpdater.SetContentType(controllerContext, "text/xml");
		}
	}
}