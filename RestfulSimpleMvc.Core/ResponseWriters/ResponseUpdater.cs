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
	}
}