using System.Xml.Linq;
using RestfulSimpleMvc.Core.SerializationDataProviders;
using RestfulSimpleMvc.Web.Models;

namespace RestfulSimpleMvc.Web.SerializationDataProviders
{
	public class AddressSerializationDataProvider : SerializationDataProvider<Address>
	{
		protected override dynamic GetJsonData(Address content) {
			return SerializeAddressToJson(content);
		}

		public static dynamic SerializeAddressToJson(Address content) {
			return new
			{
				content.ExtendedAddress,
				content.StreetAddress,
				content.Locality,
				content.PostalCode,
				content.Id,
				href = GetHref(content)
			};
		}

		protected override XDocument GetXmlData(Address content) {
			return new XDocument(SerializeAddressToXml(content));
		}

		public static XElement SerializeAddressToXml(Address content) {
			return new XElement("adr",
			                    new XAttribute("id", content.Id),
			                    new XAttribute("href", GetHref(content)),
			                    content.ExtendedAddress == null ? null : new XElement("extended-address", content.ExtendedAddress),
			                    content.StreetAddress == null ? null : new XElement("street-address", content.StreetAddress),
			                    content.Locality == null ? null : new XElement("locality", content.Locality),
			                    content.PostalCode == null ? null : new XElement("postal-code", content.PostalCode));
		}

		private static string GetHref(Address content) {
			return "http://localhost/simple-restful-mvc/addresses/" + content.Id;
		}
	}
}