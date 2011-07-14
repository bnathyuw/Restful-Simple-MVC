using NUnit.Framework;
using Playground.Mvc;
using Playground.Mvc.ResponseWriters;
using Playground.Mvc.Results;
using Rhino.Mocks;

namespace Playground.Unit.Tests.Mvc
{
	[TestFixture]
	public class TypedResultFactoryTests
	{
		private TypedResultFactory _typedResultFactory;
		private IContextResponseTypeResolver _contextResponseTypeResolver;
		private IRestfulResultFactory _restfulResultFactory;
		private IResponseWriterFactory _responseWriterFactory;

		[SetUp]
		public void SetUp() {
			_contextResponseTypeResolver = MockRepository.GenerateStub<IContextResponseTypeResolver>();
			_restfulResultFactory = MockRepository.GenerateStub<IRestfulResultFactory>();
			_responseWriterFactory = MockRepository.GenerateStub<IResponseWriterFactory>();
			_typedResultFactory = new TypedResultFactory(_contextResponseTypeResolver, _restfulResultFactory, _responseWriterFactory);
		}

		[Test]
		public void CreateCallsRestfulResultFactoryWithWriterBuiltByResponseWriterFactory() {
			IResponseWriter responseWriter = new HtmlResponseWriter();
			_responseWriterFactory.Stub(f => f.Build(Arg<ResponseType>.Is.Anything)).Return(responseWriter);

			var actionReturnValue = new object();
			_typedResultFactory.Build(null, actionReturnValue, null);

			_restfulResultFactory.AssertWasCalled(f => f.Build(responseWriter, actionReturnValue, null));
		}

		[Test]
		public void CreateReturnsValueFromRestfulResultFactory() {
			var restfulResult = new RestfulResult(null, null, null);
			_restfulResultFactory.Stub(f => f.Build(Arg<IResponseWriter>.Is.Anything, Arg<object>.Is.Anything, Arg<string>.Is.Anything)).Return(restfulResult);

			var actionResult = _typedResultFactory.Build(null, null, null);

			Assert.That(actionResult, Is.EqualTo(restfulResult));
		}

		
	}
}