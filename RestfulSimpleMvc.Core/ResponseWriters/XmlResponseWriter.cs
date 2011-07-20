using System.Web.Mvc;
using System.Xml;
using RestfulSimpleMvc.Core.SerializationDataProviders;
using StructureMap;

namespace RestfulSimpleMvc.Core.ResponseWriters
{
	public class XmlResponseWriter : IResponseWriter {
		private readonly IContainer _container;

    	public XmlResponseWriter(IContainer container) {
    		_container = container;
    	}

		public void WriteResponse(ControllerContext controllerContext, object content, string viewName) {
			var serializer = _container.ForGenericType(typeof(SerializationDataProvider<>)).WithParameters(content.GetType()).GetInstanceAs<ISerializationDataProvider>();
			var xDocument = serializer.GetXmlData(content);
			var xmlWriter = XmlWriter.Create(controllerContext.HttpContext.Response.OutputStream);
			xDocument.WriteTo(xmlWriter);
			xmlWriter.Close();
			controllerContext.HttpContext.Response.ContentType = "text/xml";
		}
	}
}