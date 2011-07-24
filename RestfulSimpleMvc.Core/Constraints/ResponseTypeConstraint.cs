using System;
using System.Web;
using System.Web.Routing;

namespace RestfulSimpleMvc.Core.Constraints
{
    public class ResponseTypeConstraint:IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            ResponseType.ResponseType responseType;
            return Enum.TryParse(values[parameterName].ToString(), true, out responseType);
        }
    }
}