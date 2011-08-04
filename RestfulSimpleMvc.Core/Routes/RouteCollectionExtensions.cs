using System.Web.Routing;
using RestfulSimpleMvc.Core.Configuration;

namespace RestfulSimpleMvc.Core.Routes
{
	public static class RouteCollectionExtensions
	{
		private static readonly IActionMapper _actionMapper;
		private static readonly IResponseTypeMapper _responseTypeMapper;

		static RouteCollectionExtensions() {
			var container = StructureMapBootstrapper.Container;
			_actionMapper = container.GetInstance<IActionMapper>();
			_responseTypeMapper = container.GetInstance<IResponseTypeMapper>();
		}

		public static void MapResource(this RouteCollection routeCollection, string url, string controller) {
		 	routeCollection.Add(new RestfulRoute(url, controller, _responseTypeMapper, _actionMapper));
		 }
	}
}