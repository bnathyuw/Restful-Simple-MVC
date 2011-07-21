using System.Web.Mvc;

namespace RestfulSimpleMvc.Core.StatusCodes
{
	public interface IStatusCodeWriter
	{
		void WriteStatusCode(ControllerContext context, object content);
	}
}