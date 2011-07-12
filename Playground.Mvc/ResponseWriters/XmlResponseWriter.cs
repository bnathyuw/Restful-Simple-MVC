using System.Web.Mvc;
using System.Xml.Serialization;

namespace Playground.Mvc.ResponseWriters
{
	public class XmlResponseWriter : IResponseWriter {
		public void WriteResponse(ControllerContext controllerContext, object content, string viewName) {
			var xmlSerializer = new XmlSerializer(content.GetType());
            var response = controllerContext.HttpContext.Response;
			var stream = response.OutputStream;
			xmlSerializer.Serialize(stream, content);
			response.ContentType = "text/xml";
		}
	}
}