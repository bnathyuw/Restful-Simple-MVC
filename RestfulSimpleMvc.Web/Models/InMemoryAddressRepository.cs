using System.Collections.Generic;
using System.Linq;

namespace RestfulSimpleMvc.Web.Models
{
	public class InMemoryAddressRepository:IAddressRepository
	{
		private static readonly List<Address> _addresses;

		static InMemoryAddressRepository()
		{
			_addresses = new List<Address> { 
				new Address { Id = 1, StreetAddress = "10 Downing Street", Locality = "London", PostalCode = "SW1A 2AA" },
				new Address { Id = 2, StreetAddress = "11 Downing Street", Locality = "London", PostalCode = "SW1A 2AB" },
				new Address { Id = 3, ExtendedAddress = "BBC Broadcasting House", StreetAddress = "Portland Place", Locality = "London", PostalCode = "W1A 1AA" },
				new Address { Id = 4, ExtendedAddress = "BBC Television Centre", StreetAddress = "Wood Land", Locality = "London", PostalCode = "W12 7RJ" }
			};
		}

		public Address Get(int id) {
			return _addresses.Single(a => a.Id == id);
		}

		public IEnumerable<Address> GetAll() {
			return _addresses;
		}

		public void Save(Address address) {
			if (address.Id > 0)
			{
				var oldAddress = _addresses.SingleOrDefault(a => a.Id == address.Id);
				_addresses.Remove(oldAddress);
			}
			else {
				address.Id = _addresses.Max(a => a.Id) + 1;
			}
			_addresses.Add(address);
		}

		public void Delete(Address address) {
			_addresses.Remove(address);
		}
	}
}