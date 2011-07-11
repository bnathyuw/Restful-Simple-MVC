using System.Runtime.Serialization;
using System.Web;

namespace Playground.Mvc.ResponseWriters
{
	public class XmlResponseWriter : IResponseWriter {
		public void WriteResponse(HttpResponseBase response, object content) {
			var xmlSerializer = new DataContractSerializer(content.GetType());
			var stream = response.OutputStream;
			xmlSerializer.WriteObject(stream, content);
			response.ContentType = "text/xml";
		}
	}
}