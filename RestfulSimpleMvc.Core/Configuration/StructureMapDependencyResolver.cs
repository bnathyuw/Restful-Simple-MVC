using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace RestfulSimpleMvc.Core.Configuration
{
	public class StructureMapDependencyResolver : IDependencyResolver
	{
	    private readonly IContainer _container;

	    public StructureMapDependencyResolver(IContainer container)
	    {
	        _container = container;
	    }

	    public object GetService(Type serviceType) {
			return serviceType.IsClass ? GetConcreteService(serviceType) : GetInterfaceService(serviceType);
		}

		private object GetConcreteService(Type serviceType) {
			try {
                return _container.GetInstance(serviceType);
			} catch (StructureMapException) {
				return null;
			}
		}

		private object GetInterfaceService(Type serviceType) {
            return _container.TryGetInstance(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
            return _container.GetAllInstances(serviceType).Cast<object>();
		}
	}
}
