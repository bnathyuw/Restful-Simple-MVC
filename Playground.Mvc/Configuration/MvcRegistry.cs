using System.Web.Mvc;
using Playground.Mvc.Exceptions;
using Playground.Mvc.ResponseWriters;
using Playground.Mvc.SerializationDataProviders;
using StructureMap.Configuration.DSL;

namespace Playground.Mvc.Configuration
{
	public class MvcRegistry:Registry
	{
		public MvcRegistry() {
			Scan(x =>
			{
				x.TheCallingAssembly();
				x.WithDefaultConventions();
			});

			For<IContextResponseTypeResolver>().Use(new ContextResponseTypeResolver(new RouteDataResponseTypeResolver(), new AcceptHeaderResponseTypeResolver(new AcceptHeaderParser(),new EnumNameParser<ResponseType>() )));
			
			For<IActionInvoker>().Use<RestfulActionInvoker>();
			SetAllProperties(c => c.OfType<IActionInvoker>());

			For<IResponseWriter>().Use<HtmlResponseWriter>().Named(ResponseType.Html.ToString());
			For<IResponseWriter>().Use<XmlResponseWriter>().Named(ResponseType.Xml.ToString());
			For<IResponseWriter>().Use<JsonResponseWriter>().Named(ResponseType.Json.ToString());

			For<ISerializationDataProvider<AmbiguousException>>().Use<RestfulExceptionSerializationDataProvider>();
			For<ISerializationDataProvider<BadGatewayException>>().Use<RestfulExceptionSerializationDataProvider>();
			For<ISerializationDataProvider<InternalServerErrorException>>().Use<RestfulExceptionSerializationDataProvider>();
			For<ISerializationDataProvider<NotFoundException>>().Use<RestfulExceptionSerializationDataProvider>();
			For<ISerializationDataProvider<RestfulException>>().Use<RestfulExceptionSerializationDataProvider>();
		}
	}
}