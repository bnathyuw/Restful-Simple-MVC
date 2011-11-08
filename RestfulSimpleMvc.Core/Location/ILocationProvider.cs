using System.Web.Mvc;

namespace RestfulSimpleMvc.Core.Location {
	public interface ILocationProvider {
		string GetLocation(object content, ControllerContext context);
	}
}