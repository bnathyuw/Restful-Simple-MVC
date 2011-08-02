using System.Web.Mvc;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.StatusCodes;
using StructureMap;

namespace RestfulSimpleMvc.Core.Results
{
	public class TypedResultFactory : ITypedResultFactory
	{
	    private readonly IRestfulResultFactory _restfulResultFactory;
		private readonly IContainer _container;

		public TypedResultFactory(IRestfulResultFactory restfulResultFactory, IContainer container) {
		    _container = container;
			_restfulResultFactory = restfulResultFactory;
		}

		public ActionResult Build(ControllerContext controllerContext, object actionReturnValue, string viewName) {
			var responseType = controllerContext.RouteData.Values["responseType"];
			var responseWriter = _container.GetInstance<IResponseWriter>(responseType.ToString());
			var statusCodeWriter = _container.GetInstance<IStatusCodeWriter>();
			return _restfulResultFactory.Build(responseWriter, actionReturnValue, viewName, statusCodeWriter);
		}
	}
}