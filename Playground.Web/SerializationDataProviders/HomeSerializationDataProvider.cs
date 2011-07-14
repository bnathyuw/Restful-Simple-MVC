using System.Xml.Linq;
using Playground.Mvc.SerializationDataProviders;
using Playground.Web.Models;

namespace Playground.Web.SerializationDataProviders
{
	public class HomeSerializationDataProvider:DefaultSerializationDataProvider<Home> {

		protected override dynamic GetJsonData(Home content) {
			return new {content.Locality, content.StreetAddress};
		}

		protected override XDocument GetXmlData(Home content)
		{
			return new XDocument(
				new XElement("home",
					new XAttribute("href", "http://localhost/restful-simple-mvc"),
					new XElement("street-address", content.StreetAddress),
					new XElement("locality", content.Locality)));
		}	
	}
}