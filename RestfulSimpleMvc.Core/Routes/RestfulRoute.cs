using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulSimpleMvc.Core.Routes {
    public class RestfulRoute : RouteBase {
        private readonly Route _innerRoute;
        private readonly Route _innerRouteWithResponseType;
    	private readonly IResponseTypeMapper _responseTypeMapper;

        public RestfulRoute(string url, string controller, IResponseTypeMapper responseTypeMapper) {
            _innerRoute = CreateRoute(url, controller);
        	_innerRouteWithResponseType = CreateRoute(url + ".{rt}", controller);
        	_responseTypeMapper = responseTypeMapper;
        }

    	private static Route CreateRoute(string url, string controller) {
    		return new Route(url,
    		                 new RouteValueDictionary(new { controller }),
    		                 new RouteValueDictionary(),
    		                 new RouteValueDictionary(),
    		                 new MvcRouteHandler());
    	}

    	public override RouteData GetRouteData(HttpContextBase httpContext) {
            var routeData = _innerRouteWithResponseType.GetRouteData(httpContext) ?? _innerRoute.GetRouteData(httpContext);
            if (routeData != null) {
            	MapAction(httpContext, routeData);
            	_responseTypeMapper.MapResponseType(httpContext, routeData);
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