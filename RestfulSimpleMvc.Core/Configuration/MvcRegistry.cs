using System.Web.Mvc;
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
			     	x.ConnectImplementationsToTypesClosing(typeof (SerializationDataProvider<>));
			     });

			For<IContextResponseTypeResolver>().Use(new ContextResponseTypeResolver(new RouteDataResponseTypeResolver(), new AcceptHeaderResponseTypeResolver(new AcceptHeaderParser(),new EnumNameParser<ResponseType>() )));
			
			For<IActionInvoker>().Use<RestfulActionInvoker>();
			SetAllProperties(c => c.OfType<IActionInvoker>());
		}
	}
}