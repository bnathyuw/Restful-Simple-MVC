using System.Web.Mvc;
using System.Web.Routing;
using RestfulSimpleMvc.Core.Configuration;
using RestfulSimpleMvc.Core.Routes;

namespace RestfulSimpleMvc.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		private static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}

		private static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapResource("", "Home");
			routes.MapResource("Exceptions/{httpStatusCode}", "Exception");
			routes.MapResource("Broken", "Broken");
			routes.MapResource("Addresses/{id}", "Address");
			routes.MapResource("Addresses", "Addresses");
		}

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();

			DependencyResolver.SetResolver(new StructureMapDependencyResolver(StructureMapBootstrapper.Container));
			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}