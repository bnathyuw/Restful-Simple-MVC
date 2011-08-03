using System.Web;
using System.Web.Routing;

namespace RestfulSimpleMvc.Core.Routes
{
	public interface IResponseTypeMapper {
		void MapResponseType(HttpContextBase httpContext, RouteData routeData);
	}
}