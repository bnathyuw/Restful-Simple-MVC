using System.Web.Mvc;
using System.Web.Routing;
using RestfulSimpleMvc.Core;
using RestfulSimpleMvc.Core.Configuration;
using RestfulSimpleMvc.Core.ResponseType;
using StructureMap;

namespace RestfulSimpleMvc.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
	    private static readonly IContainer _container;
	    private static readonly AcceptHeaderResponseTypeResolver _acceptHeaderResponseTypeResolver;

	    static MvcApplication() {
	        _container = StructureMapBootstrapper.Container;
	        _acceptHeaderResponseTypeResolver = _container.GetInstance<AcceptHeaderResponseTypeResolver>();
	    }

	    private static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		private static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.Add(new RestfulRoute("", "Home", _acceptHeaderResponseTypeResolver));
            routes.Add(new RestfulRoute("Exceptions/{httpStatusCode}", "Exception", _acceptHeaderResponseTypeResolver));
            routes.Add(new RestfulRoute("Broken", "Broken", _acceptHeaderResponseTypeResolver));
            routes.Add(new RestfulRoute("Addresses/{id}", "Address", _acceptHeaderResponseTypeResolver));
            routes.Add(new RestfulRoute("Addresses", "Addresses", _acceptHeaderResponseTypeResolver));

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

		    DependencyResolver.SetResolver(new StructureMapDependencyResolver(_container));
			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}