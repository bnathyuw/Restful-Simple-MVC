using Playground.Mvc.Configuration;
using StructureMap;

namespace Playground.Web.Configuration
{
	public static class StructureMapBootstrapper
	{
		private static readonly IContainer _container;

		static StructureMapBootstrapper() {
			_container = new Container();
			_container.Configure(x =>{
			                     	x.AddRegistry(new MvcRegistry());
			                     	x.AddRegistry(new ResponseWriterRegistry());
			                     	x.AddRegistry(new SerializationRegistry());
			                     	x.AddRegistry(new SerializerRegistry());
			                     });
		}

		public static IContainer Container {get { return _container; }}
	}
}