using System.Xml.Linq;
using Playground.Mvc.Exceptions;

namespace Playground.Mvc.SerializationDataProviders
{
	public class NotFoundExceptionSerializationDataProvider:DefaultSerializationDataProvider<NotFoundException>
	{
		protected override dynamic GetJsonData(NotFoundException content) {
			return new {content.HttpStatusCode};
		}

		protected override XDocument GetXmlData(NotFoundException content)
		{
			return new XDocument(new XElement("not-found"));
		}
	}
}