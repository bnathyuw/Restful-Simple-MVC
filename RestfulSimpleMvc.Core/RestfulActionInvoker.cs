using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestfulSimpleMvc.Core.Exceptions;
using RestfulSimpleMvc.Core.Results;
using RestfulSimpleMvc.Core.Routes;

namespace RestfulSimpleMvc.Core
{
	public class RestfulActionInvoker : ControllerActionInvoker {
		private readonly ITypedResultFactory _typedResultFactory;

		public RestfulActionInvoker(ITypedResultFactory typedResultFactory) {
			_typedResultFactory = typedResultFactory;
		}

		protected override AuthorizationContext InvokeAuthorizationFilters(ControllerContext controllerContext, IList<IAuthorizationFilter> filters, ActionDescriptor actionDescriptor) {
			CheckJsonPHasCallback(controllerContext);
			return base.InvokeAuthorizationFilters(controllerContext, filters, actionDescriptor);
		}

		protected override ActionResult CreateActionResult(ControllerContext controllerContext, ActionDescriptor actionDescriptor, object actionReturnValue) {
			var actionResult = (actionReturnValue as ActionResult)
				?? _typedResultFactory.Build(controllerContext, actionReturnValue, actionDescriptor.ActionName);

			return actionResult;
		}

		protected override ExceptionContext InvokeExceptionFilters(ControllerContext controllerContext, IList<IExceptionFilter> filters, System.Exception exception) {
			var context = new ExceptionContext(controllerContext, exception);
			foreach (var filter in filters.Reverse()) {
				filter.OnException(context);
			}
			SetExceptionResult(controllerContext, context);
			return context;
		}

		private static void CheckJsonPHasCallback(ControllerContext controllerContext) {
			var routeValueDictionary = controllerContext.RouteData.Values;

			if ((ResponseType) routeValueDictionary["responseType"] == ResponseType.JsonP 
				&& routeValueDictionary["callback"] == null) 
				throw RestfulException.BadRequest();
		}

		private void SetExceptionResult(ControllerContext controllerContext, ExceptionContext context) {
			var restfulException = context.Exception as RestfulException
			                       ?? RestfulException.InternalServerError("An internal server error has occurred", context.Exception);
			context.Result = _typedResultFactory.Build(controllerContext, restfulException, "Exception");
			context.ExceptionHandled = true;
		}
	}
}