using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestfulSimpleMvc.Web.Models;

namespace RestfulSimpleMvc.Web.Controllers
{
	public class AddressesController:Controller
	{
		private readonly IAddressRepository _repository;
		public AddressesController(IAddressRepository repository) {
			_repository = repository;
		}

		public IEnumerable<Address> Get() {
			return _repository.GetAll().AsEnumerable();
		}
	}
}