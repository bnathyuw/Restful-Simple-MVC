using System.Web;
using System.Web.Mvc;

namespace Playground.Mvc.ResponseWriters
{
	public interface IResponseWriter
	{
		void WriteResponse(ControllerContext controllerContext, object content);
	}
}