using RestfulSimpleMvc.Core.SerializationDataProviders;
using RestfulSimpleMvc.Web.Models;
using RestfulSimpleMvc.Web.SerializationDataProviders;
using StructureMap.Configuration.DSL;

namespace RestfulSimpleMvc.Web.Configuration
{
	public class SerializerRegistry:Registry
	{
		public SerializerRegistry() {
			For(typeof (ISerializationDataProvider<>)).Use(typeof(DefaultSerializationDataProvider<object>));
			For<ISerializationDataProvider<Home>>().Use<HomeSerializationDataProvider>();
		}
	}
}