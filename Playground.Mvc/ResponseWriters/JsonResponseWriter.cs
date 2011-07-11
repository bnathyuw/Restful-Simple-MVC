using System.Runtime.Serialization.Json;
using System.Web;

namespace Playground.Mvc.ResponseWriters
{
	public class JsonResponseWriter : IResponseWriter {
		public void WriteResponse(HttpResponseBase response, object content) {
			var jsonSerializer = new DataContractJsonSerializer(content.GetType());
			jsonSerializer.WriteObject(response.OutputStream, content);
			response.ContentType = "application/json";
		}
	}
}