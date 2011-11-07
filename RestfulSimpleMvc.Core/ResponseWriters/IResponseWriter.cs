using System.Web.Mvc;

namespace RestfulSimpleMvc.Core.ResponseWriters
{
	public interface IResponseWriter
	{
		void WriteResponse(ControllerContext controllerContext, object content, string viewName);
		void WriteCreated(ControllerContext controllerContext, object content);
	}
}