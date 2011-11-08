using System.Web.Mvc;

namespace RestfulSimpleMvc.Core.Results
{
	public interface ITypedResultFactory {
		ActionResult Build(ControllerContext controllerContext, object content, string viewName);
	}
}