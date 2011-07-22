using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using RestfulSimpleMvc.Core.SerializationDataProviders;
using RestfulSimpleMvc.Web.Models;

namespace RestfulSimpleMvc.Web.SerializationDataProviders
{
	public class AddressesSerializationDataProvider:SerializationDataProvider<List<Address>>
	{
		protected override dynamic GetJsonData(List<Address> content) {
			return new
			{
				Addresses = from a in content
							select AddressSerializationDataProvider.SerializeAddressToJson(a)
			};
		}

		protected override XDocument GetXmlData(List<Address> content) {
			return new XDocument(new XElement("adrs",
				from a in content
				select AddressSerializationDataProvider.SerializeAddressToXml(a)));
		}
	}
}