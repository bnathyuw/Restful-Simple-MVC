using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Playground.Mvc.ResponseWriters
{
    public class JsonResponseWriter : IResponseWriter {
        public void WriteResponse(ControllerContext controllerContext, object content, string viewName) {
        	var response = controllerContext.HttpContext.Response;
        	var jsonSerializer = new JavaScriptSerializer();
        	var output = jsonSerializer.Serialize(content);
        	response.Write(output);
        	response.ContentType = "application/json";
        }
    }
}