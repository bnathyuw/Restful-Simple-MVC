using System.Web.Mvc;
using RestfulSimpleMvc.Core.Exceptions;

namespace RestfulSimpleMvc.Core.Location {
	public class RestfulExceptionLocationProvider:LocationProvider<RestfulException> {
		protected override string GetLocation(RestfulException content, ControllerContext context) {
			throw new System.NotImplementedException();
		}
	}
}