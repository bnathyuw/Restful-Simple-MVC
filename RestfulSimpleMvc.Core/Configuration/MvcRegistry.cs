using System.Web.Mvc;
using RestfulSimpleMvc.Core.ResponseType;
using RestfulSimpleMvc.Core.SerializationDataProviders;
using RestfulSimpleMvc.Core.StatusCodes;
using StructureMap.Configuration.DSL;

namespace RestfulSimpleMvc.Core.Configuration
{
	public class MvcRegistry:Registry
	{
		public MvcRegistry() {
			Scan(x =>{
			     	x.TheCallingAssembly();
			        x.AssembliesFromApplicationBaseDirectory();
			     	x.WithDefaultConventions();
			     	x.Convention<ResponseWriterConvention>();
			     	x.Convention<StatusCodeWriterConvention>();
			     	x.ConnectImplementationsToTypesClosing(typeof (SerializationDataProvider<>));
			     });

			For<IContextResponseTypeResolver>().Use(new ContextResponseTypeResolver(new RouteDataResponseTypeResolver(), new AcceptHeaderResponseTypeResolver(new AcceptHeaderParser(),new EnumNameParser<ResponseType.ResponseType>() )));
			For<IStatusCodeWriter>().MissingNamedInstanceIs.TheInstanceNamed("Default");
			For<IActionInvoker>().Use<RestfulActionInvoker>();
			SetAllProperties(c => c.OfType<IActionInvoker>());
		}
	}
}