using System.Net;
using System.Web.Mvc;

namespace RestfulSimpleMvc.Core.ResponseWriters
{
    public class HtmlResponseWriter : IResponseWriter {
    	private readonly IResponseUpdater _responseUpdater;

    	public HtmlResponseWriter(IResponseUpdater responseUpdater) {
    		_responseUpdater = responseUpdater;
    	}

    	public void WriteResponse(ControllerContext controllerContext, object content, string viewName)
        {
			var viewEngineResult = ViewEngines.Engines.FindView(controllerContext, viewName, null);
            var textWriter = controllerContext.HttpContext.Response.Output;
            var view = viewEngineResult.View;
            var viewData = new ViewDataDictionary {Model = content};
            var tempData = new TempDataDictionary();
            var viewContext = new ViewContext(controllerContext, view, viewData, tempData, textWriter);
            view.Render(viewContext, textWriter);
        }

    	public void WriteCreated(ControllerContext controllerContext, object content) {
			_responseUpdater.SetStatusCode(controllerContext, HttpStatusCode.MovedPermanently);
		}
    }
}