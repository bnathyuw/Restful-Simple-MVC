using System.Web.Mvc;
using System.Web.Routing;
using Playground.Web.Configuration;

namespace Playground.Web
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

			routes.MapRoute(
				"Root",
				"",
				new { controller = "Home", action = "Get"});

			routes.MapRoute(
				"RootWithType",
				".{responseType}",
				new { controller = "Home", action = "Get" });

			routes.MapRoute(
				"ExceptionWithType",
				"Exceptions/{httpStatusCode}.{responseType}",
				new { controller = "Exception", action = "Get" });

			routes.MapRoute(
				"Exception",
				"Exceptions/{httpStatusCode}",
				new { controller = "Exception", action = "Get" });

			routes.MapRoute(
				"BrokenWithType",
				"Broken.{responseType}",
				new { controller = "Broken", action = "Get" });
			
			routes.MapRoute(
				"Broken",
				"Broken",
				new {controller = "Broken", action = "Get" });
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			
			DependencyResolver.SetResolver(new StructureMapDependencyResolver(new StructureMap.Container()));
			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}