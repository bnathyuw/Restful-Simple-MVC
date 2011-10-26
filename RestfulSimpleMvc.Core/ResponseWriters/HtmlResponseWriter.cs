using System.Net;
using System.Web.Mvc;

namespace RestfulSimpleMvc.Core.ResponseWriters
{
    public class HtmlResponseWriter : IResponseWriter {
        public void WriteResponse(ControllerContext controllerContext, object content, string viewName)
        {
			if (controllerContext.RouteData.Values["action"].ToString() == "POST") {
				controllerContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.MovedPermanently;
				if (content is ILocated) {
					controllerContext.HttpContext.Response.Headers.Add("Location", ((ILocated)content).GetLocation());
				} 
				return;
			} 
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