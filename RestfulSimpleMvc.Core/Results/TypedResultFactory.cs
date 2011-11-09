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
		private readonly ILocationProviderFactory _locationProviderFactory;

		public TypedResultFactory(IRestfulResultFactory restfulResultFactory, IContainer container, ILocationProviderFactory locationProviderFactory) {
		    _container = container;
			_locationProviderFactory = locationProviderFactory;
			_restfulResultFactory = restfulResultFactory;
		}

		public ActionResult Build(ControllerContext controllerContext, object content, string viewName) {
			var responseType = controllerContext.RouteData.Values["responseType"];
			var responseWriter = _container.GetInstance<IResponseWriter>(responseType.ToString());
			var statusCodeProvider = _container.GetInstance<IStatusCodeTranslator>(responseType.ToString());
			var locationProvider = _locationProviderFactory.Build(content);
			return _restfulResultFactory.Build(responseWriter, content, viewName, statusCodeProvider, locationProvider);
		}
	}
}