using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulSimpleMvc.Core
{
	public class RestfulRoute:RouteBase
	{
		private readonly Route _innerRoute;
		public RestfulRoute(string url, string controller) {
			_innerRoute = new Route(url, 
				new RouteValueDictionary(new{controller}), 
				new RouteValueDictionary(),
				new RouteValueDictionary(),
				new MvcRouteHandler());
		}

		public override RouteData GetRouteData(HttpContextBase httpContext) {
			var routeData = _innerRoute.GetRouteData(httpContext);
			if (routeData != null) {
				MapAction(httpContext, routeData);
			}
			return routeData;
		}

		private static void MapAction(HttpContextBase httpContext, RouteData routeData) {
			var httpMethod = httpContext.Request.HttpMethod;
			if (httpMethod.ToUpperInvariant() == "POST") {
				httpMethod = httpContext.Request.Form["_action"] ?? httpMethod;
			}
			routeData.Values.Add("action", httpMethod);
		}

		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values) {
			if (values.ContainsKey("action")) values.Remove("action");
			var virtualPathData = _innerRoute.GetVirtualPath(requestContext, values);
			return virtualPathData;
		}
	}
}