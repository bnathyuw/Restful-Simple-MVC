using System.Web.Mvc;
using System.Xml.Serialization;
using Playground.Mvc.Serializers;
using StructureMap;

namespace Playground.Mvc.ResponseWriters
{
	public class XmlResponseWriter : IResponseWriter {
		private readonly IContainer _container;

    	public XmlResponseWriter(IContainer container) {
    		_container = container;
    	}

		public void WriteResponse(ControllerContext controllerContext, object content, string viewName) {
			var serializer = _container.ForGenericType(typeof(ISerializer<>)).WithParameters(content.GetType()).GetInstanceAs<ISerializer>();
            var response = controllerContext.HttpContext.Response;
			serializer.WriteXmlToStream(content, response.OutputStream);
			response.ContentType = "text/xml";
		}
	}
}