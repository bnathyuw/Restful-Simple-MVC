using System.Web.Mvc;
using RestfulSimpleMvc.Core.Location;
using RestfulSimpleMvc.Web.Models;

namespace RestfulSimpleMvc.Web.Location {
	public class AddressLocationProvider:LocationProvider<Address> {
		protected override string GetLocation(Address content, ControllerContext context) {
			throw new System.NotImplementedException();
		}
	}
	
	public class HomeLocationProvider : LocationProvider<Home> {
		protected override string GetLocation(Home content, ControllerContext context) {
			throw new System.NotImplementedException();
		}
	}
	
	public class MethodsLocationProvider : LocationProvider<Method> {
		protected override string GetLocation(Method content, ControllerContext context) {
			throw new System.NotImplementedException();
		}
	}
}