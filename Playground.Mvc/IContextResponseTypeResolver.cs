using System.Web.Mvc;

namespace Playground.Mvc
{
	public interface IContextResponseTypeResolver {
		ResponseType Resolve(ControllerContext controllerContext);
	}
}