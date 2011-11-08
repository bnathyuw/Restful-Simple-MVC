using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using RestfulSimpleMvc.Core.Location;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.Results;
using RestfulSimpleMvc.Core.StatusCodes;
using Rhino.Mocks;
using StructureMap;

namespace RestfulSimpleMvc.Unit.Tests.Results
{
	[TestFixture]
	public class TypedResultFactoryTests
	{
		private TypedResultFactory _typedResultFactory;
	    private IRestfulResultFactory _restfulResultFactory;
		private IContainer _container;
	    private ControllerContext _controllerContext;
	    private RouteData _routeData;
		private IResponseUpdater _responseUpdater;
		private ILocationProviderFactory _locationProviderFactory;

		[SetUp]
		public void SetUp() {
			_restfulResultFactory = MockRepository.GenerateStub<IRestfulResultFactory>();
			
			_container = MockRepository.GenerateStub<IContainer>();
			_responseUpdater = MockRepository.GenerateStub<IResponseUpdater>();
			_locationProviderFactory = MockRepository.GenerateStub<ILocationProviderFactory>();
			_typedResultFactory = new TypedResultFactory(_restfulResultFactory, _container, _responseUpdater, _locationProviderFactory);
	    	
			_routeData = new RouteData();
	    	_routeData.Values.Add("responseType", Core.Routes.ResponseType.Xml);

	    	_controllerContext = MockRepository.GenerateStub<ControllerContext>();
	    	_controllerContext.RouteData = _routeData;
	    }

		[Test]
		public void CreateCallsRestfulResultFactoryWithWriterBuiltByResponseWriterFactory() {
			IResponseWriter responseWriter = new HtmlResponseWriter();
			_container.Stub(c => c.GetInstance<IResponseWriter>(Arg<string>.Is.Anything)).Return(responseWriter);
			var actionReturnValue = new object();
			_typedResultFactory.Build(_controllerContext, actionReturnValue, null);

			_restfulResultFactory.AssertWasCalled(f => f.Build(responseWriter, actionReturnValue, null, _responseUpdater, null, null));
		}

		[Test]
		public void CreateReturnsValueFromRestfulResultFactory() {
			var restfulResult = new RestfulResult(null, null, null, _responseUpdater, null, null);
			_restfulResultFactory.Stub(f => f.Build(Arg<IResponseWriter>.Is.Anything, Arg<object>.Is.Anything, Arg<string>.Is.Anything, Arg<IResponseUpdater>.Is.Anything, Arg<IStatusCodeTranslator>.Is.Anything, Arg<ILocationProvider>.Is.Anything)).Return(restfulResult);
            var actionResult = _typedResultFactory.Build(_controllerContext, null, null);

			Assert.That(actionResult, Is.EqualTo(restfulResult));
		}

		
	}
}