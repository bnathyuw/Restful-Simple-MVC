﻿using System.Web.Mvc;
using System.Web.Routing;
using RestfulSimpleMvc.Core.Configuration;
using RestfulSimpleMvc.Core.Routes;
using StructureMap;

namespace RestfulSimpleMvc.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
	    private static readonly IContainer _container;
	    private static readonly IResponseTypeMapper _responseTypeMapper;
		private static readonly IActionMapper _actionMapper;

	    static MvcApplication() {
	        _container = StructureMapBootstrapper.Container;
	        _responseTypeMapper = _container.GetInstance<IResponseTypeMapper>();
	    	_actionMapper = _container.GetInstance<IActionMapper>();
	    }

	    private static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		private static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.Add(RestfulRoute("", "Home"));
            routes.Add(RestfulRoute("Exceptions/{httpStatusCode}", "Exception"));
            routes.Add(RestfulRoute("Broken", "Broken"));
            routes.Add(RestfulRoute("Addresses/{id}", "Address"));
            routes.Add(RestfulRoute("Addresses", "Addresses"));
		}

	    private static RestfulRoute RestfulRoute(string url, string controller) {
	        return new RestfulRoute(url, controller, _responseTypeMapper, _actionMapper);
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