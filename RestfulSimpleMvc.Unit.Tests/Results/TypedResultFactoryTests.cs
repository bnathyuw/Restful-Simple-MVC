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

	    [SetUp]
		public void SetUp() {
			_restfulResultFactory = MockRepository.GenerateStub<IRestfulResultFactory>();
			_container = MockRepository.GenerateStub<IContainer>();
			_typedResultFactory = new TypedResultFactory(_restfulResultFactory, _container);
	        _controllerContext = MockRepository.GenerateStrictMock<ControllerContext>();
	        _routeData = new RouteData();
            _routeData.Values.Add("responseType", Core.Routes.ResponseType.Xml);
	        _controllerContext.Stub(c => c.RouteData).Return(_routeData);
	    }

		[Test]
		public void CreateCallsRestfulResultFactoryWithWriterBuiltByResponseWriterFactory() {
			IResponseWriter responseWriter = new HtmlResponseWriter();
			_container.Stub(c => c.GetInstance<IResponseWriter>(Arg<string>.Is.Anything)).Return(responseWriter);
			IStatusCodeWriter statusCodeWriter = new DefaultStatusCodeWriter();
			_container.Stub(c => c.GetInstance<IStatusCodeWriter>()).Return(statusCodeWriter);
			var actionReturnValue = new object();
			_typedResultFactory.Build(_controllerContext, actionReturnValue, null);

			_restfulResultFactory.AssertWasCalled(f => f.Build(responseWriter, actionReturnValue, null, statusCodeWriter));
		}

		[Test]
		public void CreateReturnsValueFromRestfulResultFactory() {
			var restfulResult = new RestfulResult(null, null, null, null);
			_restfulResultFactory.Stub(f => f.Build(Arg<IResponseWriter>.Is.Anything, Arg<object>.Is.Anything, Arg<string>.Is.Anything, Arg<IStatusCodeWriter>.Is.Anything)).Return(restfulResult);

            var actionResult = _typedResultFactory.Build(_controllerContext, null, null);

			Assert.That(actionResult, Is.EqualTo(restfulResult));
		}

		
	}
}