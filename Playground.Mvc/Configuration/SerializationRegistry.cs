using Playground.Mvc.Exceptions;
using Playground.Mvc.SerializationDataProviders;
using StructureMap.Configuration.DSL;

namespace Playground.Mvc.Configuration
{
	public class SerializationRegistry:Registry
	{
		public SerializationRegistry()
		{
			For<ISerializationDataProvider<AmbiguousException>>().Use<RestfulExceptionSerializationDataProvider>();
			For<ISerializationDataProvider<BadGatewayException>>().Use<RestfulExceptionSerializationDataProvider>();
			For<ISerializationDataProvider<InternalServerErrorException>>().Use<RestfulExceptionSerializationDataProvider>();
			For<ISerializationDataProvider<NotFoundException>>().Use<RestfulExceptionSerializationDataProvider>();
			For<ISerializationDataProvider<RestfulException>>().Use<RestfulExceptionSerializationDataProvider>();
			
		}
	}
}