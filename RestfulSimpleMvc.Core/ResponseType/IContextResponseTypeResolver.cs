using System.Web.Mvc;

namespace RestfulSimpleMvc.Core.ResponseType
{
	public interface IContextResponseTypeResolver {
		ResponseType Resolve(ControllerContext controllerContext);
	}
}