using StructureMap;

namespace RestfulSimpleMvc.Core.Serialization
{
	public class SerializationDataProviderFactory:ISerializationDataProviderFactory
	{
		private readonly IContainer _container;
		public SerializationDataProviderFactory(IContainer container) {
			_container = container;
		}

		public ISerializationDataProvider Build(object content) {
			return _container.ForGenericType(typeof(SerializationDataProvider<>)).WithParameters(content.GetType()).GetInstanceAs<ISerializationDataProvider>();
		}
	}
}