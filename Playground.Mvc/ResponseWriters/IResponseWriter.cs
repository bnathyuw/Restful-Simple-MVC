using System.Web;

namespace Playground.Mvc.ResponseWriters
{
	public interface IResponseWriter
	{
		void WriteResponse(HttpResponseBase response, object content);
	}
}