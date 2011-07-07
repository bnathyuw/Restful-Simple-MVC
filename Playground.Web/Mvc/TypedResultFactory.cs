using System.Web.Mvc;

namespace Playground.Web.Mvc
{
	public class TypedResultFactory : ITypedResultFactory
	{
		private readonly IContextResponseTypeResolver _contextResponseTypeResolver;

		public TypedResultFactory(IContextResponseTypeResolver contextResponseTypeResolver)
		{
			_contextResponseTypeResolver = contextResponseTypeResolver;
		}

		public ActionResult Build(ControllerContext controllerContext, object actionReturnValue, string viewName)
		{
			switch (_contextResponseTypeResolver.Resolve(controllerContext))
			{
				case ResponseType.Html:
					var viewData = new ViewDataDictionary { Model = actionReturnValue };
					return new ViewResult { ViewData = viewData, ViewName = viewName };
				case ResponseType.Xml:
					return new ContentResult { Content = "<result/>", ContentType = "text/xml" };
				case ResponseType.Json:
					return new JsonResult { Data = actionReturnValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
				default:
					return new HttpNotFoundResult();
			}
		}
	}
}