using Playground.Mvc.ResponseWriters;
using StructureMap.Configuration.DSL;

namespace Playground.Mvc.Configuration
{
	public class ResponseWriterRegistry:Registry
	{
		public ResponseWriterRegistry() {
			For<IResponseWriter>().Use<HtmlResponseWriter>().Named(ResponseType.Html.ToString());
			For<IResponseWriter>().Use<XmlResponseWriter>().Named(ResponseType.Xml.ToString());
			For<IResponseWriter>().Use<JsonResponseWriter>().Named(ResponseType.Json.ToString());
			

		}
	}
}