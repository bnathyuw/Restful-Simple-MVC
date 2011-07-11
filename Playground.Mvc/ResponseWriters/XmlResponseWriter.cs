using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;

namespace Playground.Mvc.ResponseWriters
{
	public class XmlResponseWriter : IResponseWriter {
		public void WriteResponse(ControllerContext controllerContext, object content) {
			var xmlSerializer = new DataContractSerializer(content.GetType());
            var response = controllerContext.HttpContext.Response;
			var stream = response.OutputStream;
			xmlSerializer.WriteObject(stream, content);
			response.ContentType = "text/xml";
		}
	}
}