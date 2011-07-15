using System.Web.Mvc;
using Playground.Mvc.Exceptions;
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
				x.Convention<ResponseWriterConvention>();
			});

			For<IContextResponseTypeResolver>().Use(new ContextResponseTypeResolver(new RouteDataResponseTypeResolver(), new AcceptHeaderResponseTypeResolver(new AcceptHeaderParser(),new EnumNameParser<ResponseType>() )));
			
			For<IActionInvoker>().Use<RestfulActionInvoker>();
			SetAllProperties(c => c.OfType<IActionInvoker>());

			For<ISerializationDataProvider<AmbiguousException>>().Use<RestfulExceptionSerializationDataProvider>();
			For<ISerializationDataProvider<BadGatewayException>>().Use<RestfulExceptionSerializationDataProvider>();
			For<ISerializationDataProvider<InternalServerErrorException>>().Use<RestfulExceptionSerializationDataProvider>();
			For<ISerializationDataProvider<NotFoundException>>().Use<RestfulExceptionSerializationDataProvider>();
			For<ISerializationDataProvider<RestfulException>>().Use<RestfulExceptionSerializationDataProvider>();
		}
	}
}