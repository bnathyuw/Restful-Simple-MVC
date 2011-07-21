using System;
using System.Web.Mvc;

namespace RestfulSimpleMvc.Core.StatusCodes
{
	public class HtmlStatusCodeWriter:IStatusCodeWriter
	{
		public void WriteStatusCode(ControllerContext context, object content) {
			var statusCoded = content as IStatusCoded;
			if (statusCoded != null)
			{
				context.HttpContext.Response.StatusCode = (Int32)statusCoded.HttpStatusCode;
			}
		}
	}
}