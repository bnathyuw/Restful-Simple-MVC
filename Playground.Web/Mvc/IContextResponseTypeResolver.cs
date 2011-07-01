using System.Web.Mvc;

namespace Playground.Web.Mvc
{
	public interface IContextResponseTypeResolver {
		ResponseType Resolve(ControllerContext controllerContext);
	}
}