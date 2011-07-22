using System.Web.Mvc;
using RestfulSimpleMvc.Web.Models;

namespace RestfulSimpleMvc.Web.Controllers
{
	public class AddressController:Controller
	{
		private readonly IAddressRepository _repository;

		public AddressController(IAddressRepository repository) {
			_repository = repository;
		}

		public Address Get(int id) {
			return _repository.Get(id);
		}

		public void Put(int id, Address address) {
			address.Id = id;
			_repository.Save(address);
		}
	}
}