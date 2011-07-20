using System.Web.Mvc;
using RestfulSimpleMvc.Core.Exceptions;
using RestfulSimpleMvc.Core.SerializationDataProviders;
using StructureMap.Configuration.DSL;

namespace RestfulSimpleMvc.Core.Configuration
{
	public class MvcRegistry:Registry
	{
		public MvcRegistry() {
			Scan(x =>{
			     	x.TheCallingAssembly();
			     	x.WithDefaultConventions();
			     	x.Convention<ResponseWriterConvention>();
			     	x.ConnectImplementationsToTypesClosing(typeof (ISerializationDataProvider<>));
			     });

			For<IContextResponseTypeResolver>().Use(new ContextResponseTypeResolver(new RouteDataResponseTypeResolver(), new AcceptHeaderResponseTypeResolver(new AcceptHeaderParser(),new EnumNameParser<ResponseType>() )));
			
			For<IActionInvoker>().Use<RestfulActionInvoker>();
			SetAllProperties(c => c.OfType<IActionInvoker>());

			For<ISerializationDataProvider<AmbiguousException>>().Use<RestfulExceptionSerializationDataProvider>();
			For<ISerializationDataProvider<BadGatewayException>>().Use<RestfulExceptionSerializationDataProvider>();
			For<ISerializationDataProvider<InternalServerErrorException>>().Use<RestfulExceptionSerializationDataProvider>();
		}
	}
}