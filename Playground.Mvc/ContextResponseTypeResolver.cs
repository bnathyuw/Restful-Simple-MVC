using System.Web.Mvc;

namespace Playground.Mvc
{
	public class ContextResponseTypeResolver : IContextResponseTypeResolver
	{
		private readonly IResponseTypeResolver _routeDataResponseTypeResolver;
		private readonly IResponseTypeResolver _acceptHeaderResponseTypeResolver;

		public ContextResponseTypeResolver(IResponseTypeResolver routeDataResponseTypeResolver, IResponseTypeResolver acceptHeaderResponseTypeResolver) {
			_acceptHeaderResponseTypeResolver = acceptHeaderResponseTypeResolver;
			_routeDataResponseTypeResolver = routeDataResponseTypeResolver;
		}

		public ResponseType Resolve(ControllerContext controllerContext) {
			var responseType = controllerContext.RouteData.Values["responseType"] as string;
			var acceptHeader = controllerContext.RequestContext.HttpContext.Request.Headers["Accept"];
			return _routeDataResponseTypeResolver.Resolve(responseType) 
				?? _acceptHeaderResponseTypeResolver.Resolve(acceptHeader) 
				?? ResponseType.Xml;
		}
	}
}