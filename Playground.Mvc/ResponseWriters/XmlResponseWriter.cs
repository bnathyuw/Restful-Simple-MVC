using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using Playground.Mvc.SerializationDataProviders;
using StructureMap;

namespace Playground.Mvc.ResponseWriters
{
	public class XmlResponseWriter : IResponseWriter {
		private readonly IContainer _container;

    	public XmlResponseWriter(IContainer container) {
    		_container = container;
    	}

		public void WriteResponse(ControllerContext controllerContext, object content, string viewName) {
			var serializer = _container.ForGenericType(typeof(ISerializationDataProvider<>)).WithParameters(content.GetType()).GetInstanceAs<ISerializationDataProvider>();
			var xDocument = serializer.GetXmlData(content);
			var xmlWriter = XmlWriter.Create(controllerContext.HttpContext.Response.OutputStream);
			xDocument.WriteTo(xmlWriter);
			xmlWriter.Close();
			controllerContext.HttpContext.Response.ContentType = "text/xml";
		}
	}
}