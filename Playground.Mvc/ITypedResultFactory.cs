using System.Web.Mvc;

namespace Playground.Mvc
{
	public interface ITypedResultFactory {
		ActionResult Build(ControllerContext controllerContext, object actionReturnValue, string viewName);
	}
}