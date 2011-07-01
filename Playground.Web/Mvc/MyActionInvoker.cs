using System.Linq;
using System.Web.Mvc;

namespace Playground.Web.Mvc
{
	public class MyActionInvoker:ControllerActionInvoker
	{
		private readonly IContextResponseTypeResolver _contextResponseTypeResolver;

		public MyActionInvoker(IContextResponseTypeResolver contextResponseTypeResolver) {
			_contextResponseTypeResolver = contextResponseTypeResolver;
		}

		protected override ActionResult CreateActionResult(ControllerContext controllerContext, ActionDescriptor actionDescriptor, object actionReturnValue)
		{
			if (actionReturnValue == null)
				return new EmptyResult();

			var actionResult = (actionReturnValue as ActionResult) ?? CreateTypedResult(controllerContext, actionReturnValue);

			return actionResult;
		}

		private ActionResult CreateTypedResult(ControllerContext controllerContext, object actionReturnValue) {
			switch (_contextResponseTypeResolver.Resolve(controllerContext))
			{
				case ResponseType.Html:
					var viewData = new ViewDataDictionary {Model = actionReturnValue};
					return new ViewResult {ViewData = viewData};
				case ResponseType.Xml:
					return new ContentResult {Content = "<result/>", ContentType = "text/xml"};
				case ResponseType.Json:
					return new JsonResult { Data = actionReturnValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
				default:
					return new HttpNotFoundResult();
			}
		}

		protected override ExceptionContext InvokeExceptionFilters(ControllerContext controllerContext, System.Collections.Generic.IList<IExceptionFilter> filters, System.Exception exception)
		{
			var context = new ExceptionContext(controllerContext, exception);
			foreach (var filter in filters.Reverse())
			{
				filter.OnException(context);
			}

			return context;
		}
	}
}