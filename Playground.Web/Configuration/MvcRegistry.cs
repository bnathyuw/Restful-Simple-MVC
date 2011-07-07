using System.Web.Mvc;
using Playground.Web.Mvc;
using StructureMap.Configuration.DSL;

namespace Playground.Web.Configuration
{
	internal class MvcRegistry:Registry
	{
		public MvcRegistry() {

			For<IContextResponseTypeResolver>().Use(new ContextResponseTypeResolver(new RouteDataResponseTypeResolver(), new AcceptHeaderResponseTypeResolver(new AcceptHeaderParser(),new EnumNameParser<ResponseType>() )));
			For<IActionInvoker>().Use<RestfulActionInvoker>();
			For<ITypedResultFactory>().Use<TypedResultFactory>();
			SetAllProperties(c => c.OfType<IActionInvoker>());
		}
	}
}