using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
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

		[SetUp]
		public void SetUp() {
			_restfulResultFactory = MockRepository.GenerateStub<IRestfulResultFactory>();
			
			_container = MockRepository.GenerateStub<IContainer>();
			_responseUpdater = MockRepository.GenerateStub<IResponseUpdater>();
			_typedResultFactory = new TypedResultFactory(_restfulResultFactory, _container, _responseUpdater);
	    	
			_routeData = new RouteData();
	    	_routeData.Values.Add("responseType", Core.Routes.ResponseType.Xml);

	    	_controllerContext = MockRepository.GenerateStub<ControllerContext>();
	    	_controllerContext.RouteData = _routeData;
	    }

		[Test]
		public void CreateCallsRestfulResultFactoryWithWriterBuiltByResponseWriterFactory() {
			var responseUpdater = MockRepository.GenerateStub<IResponseUpdater>();
			IResponseWriter responseWriter = new HtmlResponseWriter(responseUpdater);
			_container.Stub(c => c.GetInstance<IResponseWriter>(Arg<string>.Is.Anything)).Return(responseWriter);
			IStatusCodeWriter statusCodeWriter = new DefaultStatusCodeWriter();
			_container.Stub(c => c.GetInstance<IStatusCodeWriter>()).Return(statusCodeWriter);
			var actionReturnValue = new object();
			_typedResultFactory.Build(_controllerContext, actionReturnValue, null);

			_restfulResultFactory.AssertWasCalled(f => f.Build(responseWriter, actionReturnValue, null, _responseUpdater));
		}

		[Test]
		public void CreateReturnsValueFromRestfulResultFactory() {
			var restfulResult = new RestfulResult(null, null, null, _responseUpdater);
			_restfulResultFactory.Stub(f => f.Build(Arg<IResponseWriter>.Is.Anything, Arg<object>.Is.Anything, Arg<string>.Is.Anything, Arg<IResponseUpdater>.Is.Anything)).Return(restfulResult);

            var actionResult = _typedResultFactory.Build(_controllerContext, null, null);

			Assert.That(actionResult, Is.EqualTo(restfulResult));
		}

		
	}
}