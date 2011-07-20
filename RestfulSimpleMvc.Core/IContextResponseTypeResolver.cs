using System.Web.Mvc;

namespace RestfulSimpleMvc.Core
{
	public interface IContextResponseTypeResolver {
		ResponseType Resolve(ControllerContext controllerContext);
	}
}