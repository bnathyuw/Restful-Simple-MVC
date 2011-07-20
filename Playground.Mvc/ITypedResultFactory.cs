using System.Web.Mvc;

namespace RestfulSimpleMvc.Core
{
	public interface ITypedResultFactory {
		ActionResult Build(ControllerContext controllerContext, object actionReturnValue, string viewName);
	}
}