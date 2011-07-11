using System;
using System.Web;

namespace Playground.Mvc.ResponseWriters
{
	public class HtmlResponseWriter : IResponseWriter {
		public void WriteResponse(HttpResponseBase response, object content) {
			response.Output.Write("Hello, world");
			response.ContentType = "text/html";
		}
	}
}