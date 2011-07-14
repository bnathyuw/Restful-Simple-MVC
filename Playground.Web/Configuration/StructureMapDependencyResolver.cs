using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace Playground.Web.Configuration
{
	public class StructureMapDependencyResolver : IDependencyResolver
	{
		public object GetService(Type serviceType) {
			return serviceType.IsClass ? GetConcreteService(serviceType) : GetInterfaceService(serviceType);
		}

		private static object GetConcreteService(Type serviceType) {
			try {
				return StructureMapBootstrapper.Container.GetInstance(serviceType);
			} catch (StructureMapException) {
				return null;
			}
		}

		private static object GetInterfaceService(Type serviceType) {
			return StructureMapBootstrapper.Container.TryGetInstance(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return StructureMapBootstrapper.Container.GetAllInstances(serviceType).Cast<object>();
		}
	}
}
