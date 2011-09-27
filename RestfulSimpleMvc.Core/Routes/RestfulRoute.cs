using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System;

namespace RestfulSimpleMvc.Core.Routes {
	public class RestfulRoute : RouteBase {
		private readonly Route _innerRoute;
		private readonly Route _innerRouteWithAction;
		private readonly Route _innerRouteWithResponseType;
		private readonly Route _innerRouteWithActionAndResponseType;
		private readonly IResponseTypeMapper _responseTypeMapper;
		private readonly IActionMapper _actionMapper;

		public RestfulRoute(string url, string controller, IResponseTypeMapper responseTypeMapper, IActionMapper actionMapper) {
			_innerRoute = CreateRoute(url, controller);
			_innerRouteWithAction = CreateRoute(url + "/{action}", controller);
			_innerRouteWithResponseType = CreateRoute(url + ".{rt}", controller);
			_innerRouteWithActionAndResponseType = CreateRoute(url + "/{action}.{rt}", controller);
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
			var routeData = _innerRouteWithActionAndResponseType.GetRouteData(httpContext) ?? _innerRouteWithAction.GetRouteData(httpContext) ?? _innerRouteWithResponseType.GetRouteData(httpContext) ?? _innerRoute.GetRouteData(httpContext);

			if (routeData == null) return null;

			_actionMapper.MapAction(httpContext, routeData);
			_responseTypeMapper.MapResponseType(httpContext, routeData);

			return routeData;
		}

		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values) {
			ResolveAction(values);

			_responseTypeMapper.ResolveResponseType(values, requestContext.HttpContext);

			ResolveCallback(values);

			Route route;
			if (values.ContainsKey("action")) {
				route = values.ContainsKey("rt")
					? _innerRouteWithActionAndResponseType
					: _innerRouteWithAction;
			}
			else {
				route = values.ContainsKey("rt")
					? _innerRouteWithResponseType
					: _innerRoute;
			}

			return route.GetVirtualPath(requestContext, values);
		}

		private static void ResolveCallback(IDictionary<string, object> values) {
			if (values.ContainsKey("rt") && values["rt"].ToString() == "jsonp" && !values.ContainsKey("callback")) {
				values["callback"] = "callback";
			}
		}

		private static void ResolveAction(IDictionary<string, object> values) {
			RestfulHttpVerb verb;
			if (values.ContainsKey("action") && Enum.TryParse(values["action"].ToString(), true, out verb)) {
				values.Remove("action");
			}
		}
	}

	public enum RestfulHttpVerb {
		Get,
		Post,
		Put,
		Delete,
		Head,
		Options
	}
}