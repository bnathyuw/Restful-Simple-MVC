using System.Web.Mvc;
using Playground.Mvc.Exceptions;
using Playground.Mvc.ResponseWriters;
using Playground.Mvc.Serializers;
using StructureMap.Configuration.DSL;

namespace Playground.Mvc.Configuration
{
	public class MvcRegistry:Registry
	{
		public MvcRegistry() {
			Scan(x =>{
			     	x.TheCallingAssembly();
			     	x.WithDefaultConventions();
			     });
			
			For<IContextResponseTypeResolver>().Use(new ContextResponseTypeResolver(new RouteDataResponseTypeResolver(), new AcceptHeaderResponseTypeResolver(new AcceptHeaderParser(),new EnumNameParser<ResponseType>() )));
			
			For<IActionInvoker>().Use<RestfulActionInvoker>();
			
			For<IResponseWriter>().Use<HtmlResponseWriter>().Named(ResponseType.Html.ToString());
			For<IResponseWriter>().Use<XmlResponseWriter>().Named(ResponseType.Xml.ToString());
			For<IResponseWriter>().Use<JsonResponseWriter>().Named(ResponseType.Json.ToString());

			For<ISerializer<AmbiguousException>>().Use<RestfulExceptionSerializer>();
			For<ISerializer<BadGatewayException>>().Use<RestfulExceptionSerializer>();
			For<ISerializer<InternalServerErrorException>>().Use<RestfulExceptionSerializer>();
			For<ISerializer<NotFoundException>>().Use<RestfulExceptionSerializer>();
			For<ISerializer<RestfulException>>().Use<RestfulExceptionSerializer>();
			
			SetAllProperties(c => c.OfType<IActionInvoker>());
		}
	}
}