using System.Web;
using System.Web.Routing;

namespace RestfulSimpleMvc.Core.Routes
{
	public interface IActionMapper {
		void MapAction(HttpContextBase httpContext, RouteData routeData);
	}
}