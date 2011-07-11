using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;

namespace Playground.Mvc.ResponseWriters
{
    public class JsonResponseWriter : IResponseWriter {
        public void WriteResponse(ControllerContext controllerContext, object content) {
            var response = controllerContext.HttpContext.Response;
            var jsonSerializer = new DataContractJsonSerializer(content.GetType());
            jsonSerializer.WriteObject(response.OutputStream, content);
            response.ContentType = "application/json";
        }
    }
}