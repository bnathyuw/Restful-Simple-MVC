using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;

namespace RestfulSimpleMvc.Core.Routes
{
    public class ResponseTypeMapper : IResponseTypeMapper
    {
        private readonly IAcceptHeaderResponseTypeResolver _acceptHeaderResponseTypeResolver;
        public ResponseTypeMapper(IAcceptHeaderResponseTypeResolver acceptHeaderResponseTypeResolver) {
            _acceptHeaderResponseTypeResolver = acceptHeaderResponseTypeResolver;
        }

        public void MapResponseType(HttpContextBase httpContext, RouteData routeData) {
            routeData.Values.Add("responseType", ParseResponseType(routeData) 
                ?? _acceptHeaderResponseTypeResolver.Resolve(httpContext.Request.Headers["Accept"]) 
                ?? ResponseType.Xml);
        }

        public void ResolveResponseType(IDictionary<string, object> values, HttpContextBase httpContext) {
            if (!values.ContainsKey("responseType"))
                return;

            var responseType = (ResponseType)values["responseType"];

            var requestResponseType = _acceptHeaderResponseTypeResolver.Resolve(httpContext.Request.Headers["Accept"]);

            if (requestResponseType == null) {
                if (responseType != ResponseType.Xml) {
                    values.Add("rt", responseType.ToString().ToLowerInvariant());
                }
            } else {
                if (requestResponseType != responseType) {
                    values.Add("rt", responseType.ToString().ToLowerInvariant());
                }
            }

            values.Remove("responseType");
        }
        
        private static ResponseType? ParseResponseType(RouteData routeData) {
            var rt = routeData.Values["rt"] as string;
            if (rt == null) 
                return null;

            ResponseType responseType;
            return Enum.TryParse(rt, true, out responseType) 
                ? responseType 
                : (ResponseType?) null;
        }

        
    }
}