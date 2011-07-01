using System.Web.Mvc;
using System.Web.Routing;
using Playground.Web.Configuration;

namespace Playground.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
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