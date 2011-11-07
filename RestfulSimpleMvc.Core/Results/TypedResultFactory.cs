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
		private readonly IResponseUpdater _responseUpdater;

		public TypedResultFactory(IRestfulResultFactory restfulResultFactory, IContainer container, IResponseUpdater responseUpdater) {
		    _container = container;
			_responseUpdater = responseUpdater;
			_restfulResultFactory = restfulResultFactory;
		}

		public ActionResult Build(ControllerContext controllerContext, object actionReturnValue, string viewName) {
			var responseType = controllerContext.RouteData.Values["responseType"];
			var responseWriter = _container.GetInstance<IResponseWriter>(responseType.ToString());
			var statusCodeProvider = _container.GetInstance<IStatusCodeTranslator>(responseType.ToString());
			return _restfulResultFactory.Build(responseWriter, actionReturnValue, viewName, _responseUpdater, statusCodeProvider);
		}
	}
}