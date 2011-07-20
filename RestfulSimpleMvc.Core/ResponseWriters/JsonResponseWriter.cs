using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using RestfulSimpleMvc.Core.SerializationDataProviders;
using StructureMap;

namespace RestfulSimpleMvc.Core.ResponseWriters
{
	public class JsonResponseWriter : IResponseWriter
	{
		private readonly IContainer _container;

		public JsonResponseWriter(IContainer container)
		{
			_container = container;
		}

		public void WriteResponse(ControllerContext controllerContext, object content, string viewName)
		{
			var serializer = _container.ForGenericType(typeof(SerializationDataProvider<>)).WithParameters(content.GetType()).GetInstanceAs<ISerializationDataProvider>();
			var jsonData = serializer.GetJsonData(content);
			var javaScriptSerializer = new JavaScriptSerializer();
			var serialize = javaScriptSerializer.Serialize(jsonData);
			var bytes = Encoding.UTF8.GetBytes(serialize);
			controllerContext.HttpContext.Response.OutputStream.Write(bytes, 0, bytes.Length);
			var response = controllerContext.HttpContext.Response;
			response.ContentType = "application/json";
		}
	}
}