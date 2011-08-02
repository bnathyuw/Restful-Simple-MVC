using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RestfulSimpleMvc.Core.ResponseType;

namespace RestfulSimpleMvc.Core {
    public class RestfulRoute : RouteBase {
        private readonly Route _innerRoute;
        private readonly Route _innerRouteWithResponseType;
        private readonly IAcceptHeaderResponseTypeResolver _acceptHeaderResponseTypeResolver;

        public RestfulRoute(string url, string controller, IAcceptHeaderResponseTypeResolver acceptHeaderResponseTypeResolver) {
            _innerRoute = new Route(url,
                new RouteValueDictionary(new { controller }),
                new RouteValueDictionary(),
                new RouteValueDictionary(),
                new MvcRouteHandler());
            _innerRouteWithResponseType = new Route(url + ".{rt}",
                 new RouteValueDictionary(new { controller }),
                new RouteValueDictionary(),
                new RouteValueDictionary(),
                new MvcRouteHandler());
            _acceptHeaderResponseTypeResolver = acceptHeaderResponseTypeResolver;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext) {
            var routeData = _innerRouteWithResponseType.GetRouteData(httpContext) ?? _innerRoute.GetRouteData(httpContext);
            if (routeData != null) {
                MapAction(httpContext, routeData);
                routeData.Values.Add("responseType", ParseResponseType(routeData) 
                    ?? _acceptHeaderResponseTypeResolver.Resolve(httpContext.Request.Headers["Accept"]) 
                    ?? ResponseType.ResponseType.Xml);
           }
            return routeData;
        }

        private static ResponseType.ResponseType? ParseResponseType(RouteData routeData) {
            var rt = routeData.Values["rt"];
            if (rt == null) 
                return null;

            ResponseType.ResponseType responseType;
            return Enum.TryParse(rt.ToString(), true, out responseType) ? responseType : (ResponseType.ResponseType?) null;
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