using System.Web.Mvc;
using RestfulSimpleMvc.Core.ResponseType;
using RestfulSimpleMvc.Core.ResponseWriters;
using StructureMap;

namespace RestfulSimpleMvc.Core.Results
{
	public class TypedResultFactory : ITypedResultFactory
	{
		private readonly IContextResponseTypeResolver _contextResponseTypeResolver;
		private readonly IRestfulResultFactory _restfulResultFactory;
		private readonly IContainer _container;

		public TypedResultFactory(IContextResponseTypeResolver contextResponseTypeResolver, IRestfulResultFactory restfulResultFactory, IContainer container) {
			_contextResponseTypeResolver = contextResponseTypeResolver;
			_container = container;
			_restfulResultFactory = restfulResultFactory;
		}

		public ActionResult Build(ControllerContext controllerContext, object actionReturnValue, string viewName) {
			var responseType = _contextResponseTypeResolver.Resolve(controllerContext);
			var responseWriter = _container.GetInstance<IResponseWriter>(responseType.ToString());
			return _restfulResultFactory.Build(responseWriter, actionReturnValue, viewName);
		}
	}
}