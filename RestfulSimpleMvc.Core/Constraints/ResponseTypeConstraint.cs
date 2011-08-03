using System;
using System.Web;
using System.Web.Routing;
using RestfulSimpleMvc.Core.Routes;

namespace RestfulSimpleMvc.Core.Constraints
{
    public class ResponseTypeConstraint:IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            ResponseType responseType;
            return Enum.TryParse(values[parameterName].ToString(), true, out responseType);
        }
    }
}