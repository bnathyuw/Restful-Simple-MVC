using System.Web.Mvc;

namespace Playground.Web.Mvc
{
	public interface ITypedResultFactory {
		ActionResult Build(ControllerContext controllerContext, object actionReturnValue, string viewName);
	}
}