using System.Web.Mvc;

namespace RestfulSimpleMvc.Core.Results {
	public interface IContextHelper {
		string GetRequestLocation(ControllerContext context);
	}
}