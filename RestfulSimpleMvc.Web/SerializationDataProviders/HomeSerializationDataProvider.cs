using System.Linq;
using System.Xml.Linq;
using RestfulSimpleMvc.Core.SerializationDataProviders;
using RestfulSimpleMvc.Web.Models;

namespace RestfulSimpleMvc.Web.SerializationDataProviders
{
	public class HomeSerializationDataProvider:SerializationDataProvider<Home> {

		protected override dynamic GetJsonData(Home content) {
			return new
			{
				content.Locality,
				content.StreetAddress,
				Inhabitants = from i in content.Inhabitants
							  select new { i.Name }
			};
		}

		protected override XDocument GetXmlData(Home content)
		{
			return new XDocument(
				new XElement("home",
					new XAttribute("href", "http://localhost/restful-simple-mvc"),
					new XElement("street-address", content.StreetAddress),
					new XElement("locality", content.Locality),
					new XElement("inhabitants", 
						from i in content.Inhabitants
						select new XElement("inhabitant", i.Name))));
		}	
	}
}