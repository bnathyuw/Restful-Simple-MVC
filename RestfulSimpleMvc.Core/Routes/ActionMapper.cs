using System.Web;
using System.Web.Routing;

namespace RestfulSimpleMvc.Core.Routes
{
	public class ActionMapper : IActionMapper
	{
		public void MapAction(HttpContextBase httpContext, RouteData routeData) {
			var httpMethod = httpContext.Request.HttpMethod;
			if (httpMethod.ToUpperInvariant() == "POST") {
				httpMethod = httpContext.Request.Form["_action"] ?? httpMethod;
			}
			routeData.Values.Add("action", httpMethod);
		}
	}
}