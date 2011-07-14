using System.Web.Mvc;
using Playground.Mvc.Serializers;
using StructureMap;

namespace Playground.Mvc.ResponseWriters
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
			var serializer = _container.ForGenericType(typeof(ISerializer<>)).WithParameters(content.GetType()).GetInstanceAs<ISerializer>();
			var response = controllerContext.HttpContext.Response;
			serializer.WriteJsonToStream(content, response.OutputStream);
			response.ContentType = "application/json";
		}
	}
}