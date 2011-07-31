using StructureMap;

namespace RestfulSimpleMvc.Core.Configuration
{
	public static class StructureMapBootstrapper
	{
		private static readonly IContainer _container;

		static StructureMapBootstrapper() {
			_container = new Container();
		    _container.Configure(x => x.Scan(y =>
		                                     {
		                                         y.TheCallingAssembly();
		                                         y.AssembliesFromApplicationBaseDirectory();
		                                         y.LookForRegistries();
		                                     }));
		}

		public static IContainer Container {get { return _container; }}
	}
}