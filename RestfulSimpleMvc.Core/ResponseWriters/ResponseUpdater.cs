using System.Net;
using System.Text;
using System.Web.Mvc;

namespace RestfulSimpleMvc.Core.ResponseWriters
{
	public class ResponseUpdater:IResponseUpdater
	{
		public void WriteOutputToResponse(ControllerContext controllerContext, string output) {
			var bytes = Encoding.UTF8.GetBytes(output);
			controllerContext.HttpContext.Response.OutputStream.Write(bytes, 0, bytes.Length);
		}

		public void SetContentType(ControllerContext controllerContext, string contentType) {
			controllerContext.HttpContext.Response.ContentType = contentType;
		}

		public void SetStatusCode(ControllerContext controllerContext, HttpStatusCode statusCode) {
			controllerContext.HttpContext.Response.StatusCode = (int)statusCode;
		}

		public void SetLocation(ControllerContext controllerContext	, string location) {
			controllerContext.HttpContext.Response.RedirectLocation = location;
		}
	}
}