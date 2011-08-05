using System;
using System.Xml.Linq;
using RestfulSimpleMvc.Core.Serialization;

namespace RestfulSimpleMvc.Integration.Tests.Mvc.Configuration
{
	public class EntityStubSerializationDataProvider:SerializationDataProvider<EntityStub> {
		protected override dynamic GetJsonData(EntityStub content)
		{
			throw new NotImplementedException();
		}

		protected override XDocument GetXmlData(EntityStub content)
		{
			throw new NotImplementedException();
		}
	}
}