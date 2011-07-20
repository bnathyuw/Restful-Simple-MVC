using System.Linq;
using System.Web.Mvc;
using RestfulSimpleMvc.Core.Exceptions;

namespace RestfulSimpleMvc.Core
{
	public class RestfulActionInvoker:ControllerActionInvoker
	{
		private readonly ITypedResultFactory _typedResultFactory;

		public RestfulActionInvoker(ITypedResultFactory typedResultFactory) {
			_typedResultFactory = typedResultFactory;
		}

		protected override ActionResult CreateActionResult(ControllerContext controllerContext, ActionDescriptor actionDescriptor, object actionReturnValue)
		{
			if (actionReturnValue == null)
				return new EmptyResult();

			var actionResult = (actionReturnValue as ActionResult) ?? _typedResultFactory.Build(controllerContext, actionReturnValue, actionDescriptor.ActionName);

			return actionResult;
		}

		protected override ExceptionContext InvokeExceptionFilters(ControllerContext controllerContext, System.Collections.Generic.IList<IExceptionFilter> filters, System.Exception exception)
		{
			var context = new ExceptionContext(controllerContext, exception);
			foreach (var filter in filters.Reverse())
			{
				filter.OnException(context);
			}
			SetExceptionResult(controllerContext, context);
			return context;
		}

		private void SetExceptionResult(ControllerContext controllerContext, ExceptionContext context) {
			var restfulException = context.Exception as RestfulException 
				?? new InternalServerErrorException("An internal server error has occurred", context.Exception);
			context.Result = _typedResultFactory.Build(controllerContext, restfulException, "Exception");
			context.ExceptionHandled = true;
		}
	}
}