using System.Xml.Linq;
using RestfulSimpleMvc.Core.Exceptions;

namespace RestfulSimpleMvc.Core.SerializationDataProviders
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