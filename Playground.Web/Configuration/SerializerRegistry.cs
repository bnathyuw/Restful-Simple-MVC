using Playground.Mvc.SerializationDataProviders;
using Playground.Web.Models;
using Playground.Web.SerializationDataProviders;
using StructureMap.Configuration.DSL;

namespace Playground.Web.Configuration
{
	public class SerializerRegistry:Registry
	{
		public SerializerRegistry() {
			For(typeof (ISerializationDataProvider<>)).Use(typeof(DefaultSerializationDataProvider<object>));
			For<ISerializationDataProvider<Home>>().Use<HomeSerializationDataProvider>();
		}
	}
}