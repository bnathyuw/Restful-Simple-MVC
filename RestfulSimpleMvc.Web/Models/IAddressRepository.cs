using System.Collections.Generic;

namespace RestfulSimpleMvc.Web.Models
{
	public interface IAddressRepository
	{
		Address Get(int id);
		IEnumerable<Address> GetAll();
		void Save(Address address);
		void Delete(Address address);
	}
}