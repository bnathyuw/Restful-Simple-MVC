using System.Web.Mvc;
using RestfulSimpleMvc.Core.Location;
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
		private readonly ILocationProviderFactory _locationProviderFactory;

		public TypedResultFactory(IRestfulResultFactory restfulResultFactory, IContainer container, IResponseUpdater responseUpdater, ILocationProviderFactory locationProviderFactory) {
		    _container = container;
			_responseUpdater = responseUpdater;
			_locationProviderFactory = locationProviderFactory;
			_restfulResultFactory = restfulResultFactory;
		}

		public ActionResult Build(ControllerContext controllerContext, object content, string viewName) {
			var responseType = controllerContext.RouteData.Values["responseType"];
			var responseWriter = _container.GetInstance<IResponseWriter>(responseType.ToString());
			var statusCodeProvider = _container.GetInstance<IStatusCodeTranslator>(responseType.ToString());
			var locationProvider = _locationProviderFactory.Build(content);
			return _restfulResultFactory.Build(responseWriter, content, viewName, _responseUpdater, statusCodeProvider, locationProvider);
		}
	}
}