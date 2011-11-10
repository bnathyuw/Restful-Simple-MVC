using StructureMap;

namespace RestfulSimpleMvc.Core.Location {
	public class LocationProviderFactory : ILocationProviderFactory {
		private readonly IContainer _container;
		public LocationProviderFactory(IContainer container) {
			_container = container;
		}

		public ILocationProvider Build(object content) {
			if (content == null) return null;
			return _container.ForGenericType(typeof(LocationProvider<>)).WithParameters(content.GetType()).GetInstanceAs<ILocationProvider>();
		}
	}
}