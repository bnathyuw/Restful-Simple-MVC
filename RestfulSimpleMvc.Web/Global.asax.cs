using System.Web.Mvc;
using System.Web.Routing;
using RestfulSimpleMvc.Core;
using RestfulSimpleMvc.Core.Configuration;

namespace RestfulSimpleMvc.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		private static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		private static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.Add(new RestfulRoute("", "Home"));
			routes.Add(new RestfulRoute(".{responseType}", "Home"));
			routes.Add(new RestfulRoute("Exceptions/{httpStatusCode}.{responseType}", "Exception"));
			routes.Add(new RestfulRoute("Exceptions/{httpStatusCode}", "Exception"));
			routes.Add(new RestfulRoute("Broken.{responseType}", "Broken"));
			routes.Add(new RestfulRoute("Broken", "Broken"));
			routes.Add(new RestfulRoute("Addresses/{id}.{responseType}", "Address"));
			routes.Add(new RestfulRoute("Addresses/{id}", "Address"));
			routes.Add(new RestfulRoute("Addresses.{responseType}", "Addresses"));
			routes.Add(new RestfulRoute("Addresses", "Addresses"));

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			
			DependencyResolver.SetResolver(new StructureMapDependencyResolver(StructureMapBootstrapper.Container));
			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}