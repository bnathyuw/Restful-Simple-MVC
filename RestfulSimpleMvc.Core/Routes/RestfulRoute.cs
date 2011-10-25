using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulSimpleMvc.Core.Routes {
    public class RestfulRoute : RouteBase
    {
    	private readonly Route _innerRoute;
    	private readonly Route _innerRouteWithResponseType;
    	private readonly IResponseTypeMapper _responseTypeMapper;
    	private readonly IActionMapper _actionMapper;

    	public RestfulRoute(string url, string controller, IResponseTypeMapper responseTypeMapper, IActionMapper actionMapper) {
    		_innerRoute = CreateRoute(url, controller);
    		_innerRouteWithResponseType = CreateRoute(url + ".{rt}", controller);
    		_responseTypeMapper = responseTypeMapper;
    		_actionMapper = actionMapper;
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

    		if (routeData == null) return null;
    		
    		_actionMapper.MapAction(httpContext, routeData);
    		_responseTypeMapper.MapResponseType(httpContext, routeData);
			
			if (httpContext.Request.QueryString["callback"] != null) {
				routeData.Values["callback"] = httpContext.Request.QueryString["callback"]
					?? httpContext.Request.Form["callback"];
			}
			else if (httpContext.Request.Form["callback"] != null) {
				routeData.Values["callback"] = httpContext.Request.Form["callback"];
			}

    		
			return routeData;
        }

    	public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values) {
    		ResolveAction(values);

    		_responseTypeMapper.ResolveResponseType(values, requestContext.HttpContext);

			ResolveCallback(values);

    		var route = values.ContainsKey("rt") 
				? _innerRouteWithResponseType 
				: _innerRoute;

    		return route.GetVirtualPath(requestContext, values);
    	}

    	private static void ResolveCallback(IDictionary<string, object> values) {
    		if (values.ContainsKey("rt") && values["rt"].ToString() == "jsonp" && !values.ContainsKey("callback")) {
    			values["callback"] = "callback";
    		}
    	}

    	private static void ResolveAction(IDictionary<string, object> values) {
    		if (values.ContainsKey("action")) {
    			values.Remove("action");
    		}
    	}
    }
}