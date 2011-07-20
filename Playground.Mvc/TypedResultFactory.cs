using System.Web.Mvc;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.Results;

namespace RestfulSimpleMvc.Core
{
	public class TypedResultFactory : ITypedResultFactory
	{
		private readonly IContextResponseTypeResolver _contextResponseTypeResolver;
		private readonly IRestfulResultFactory _restfulResultFactory;
		private readonly IResponseWriterFactory _responseWriterFactory;

		public TypedResultFactory(IContextResponseTypeResolver contextResponseTypeResolver, IRestfulResultFactory restfulResultFactory, IResponseWriterFactory responseWriterFactory) {
			_contextResponseTypeResolver = contextResponseTypeResolver;
			_responseWriterFactory = responseWriterFactory;
			_restfulResultFactory = restfulResultFactory;
		}

		public ActionResult Build(ControllerContext controllerContext, object actionReturnValue, string viewName) {
			var responseType = _contextResponseTypeResolver.Resolve(controllerContext);
			var responseWriter = _responseWriterFactory.Build(responseType);
			return _restfulResultFactory.Build(responseWriter, actionReturnValue, viewName);
		}
	}
}