using System.Net;
using System.Web.Mvc;

namespace RestfulSimpleMvc.Core.ResponseWriters
{
	public interface IResponseUpdater
	{
		void WriteOutputToResponse(ControllerContext controllerContext, string output);
		void SetContentType(ControllerContext controllerContext, string contentType);
		void SetStatusCode(ControllerContext controllerContext, HttpStatusCode statusCode);
	}
}