using System.Web.Mvc;

namespace RestfulSimpleMvc.Core.ResponseWriters
{
    public class HtmlResponseWriter : IResponseWriter {
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
    }
}