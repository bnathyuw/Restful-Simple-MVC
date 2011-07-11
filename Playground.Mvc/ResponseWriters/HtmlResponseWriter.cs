using System.Web.Mvc;

namespace Playground.Mvc.ResponseWriters
{
    public class HtmlResponseWriter : IResponseWriter {
        public void WriteResponse(ControllerContext controllerContext, object content)
        {
            var viewEngineResult = ViewEngines.Engines.FindView(controllerContext, controllerContext.RouteData.GetRequiredString("action"), null);
            var textWriter = controllerContext.HttpContext.Response.Output;
            var view = viewEngineResult.View;
            var viewData = new ViewDataDictionary {Model = content};
            var tempData = new TempDataDictionary();
            var viewContext = new ViewContext(controllerContext, view, viewData, tempData, textWriter);
            view.Render(viewContext, textWriter);
        }
    }
}