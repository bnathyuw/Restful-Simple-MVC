using NUnit.Framework;
using RestfulSimpleMvc.Core.ResponseType;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.Results;
using Rhino.Mocks;
using StructureMap;

namespace RestfulSimpleMvc.Unit.Tests.Results
{
	[TestFixture]
	public class TypedResultFactoryTests
	{
		private TypedResultFactory _typedResultFactory;
		private IContextResponseTypeResolver _contextResponseTypeResolver;
		private IRestfulResultFactory _restfulResultFactory;
		private IContainer _container;

		[SetUp]
		public void SetUp() {
			_contextResponseTypeResolver = MockRepository.GenerateStub<IContextResponseTypeResolver>();
			_restfulResultFactory = MockRepository.GenerateStub<IRestfulResultFactory>();
			_container = MockRepository.GenerateStub<IContainer>();
			_typedResultFactory = new TypedResultFactory(_contextResponseTypeResolver, _restfulResultFactory, _container);
		}

		[Test]
		public void CreateCallsRestfulResultFactoryWithWriterBuiltByResponseWriterFactory() {
			IResponseWriter responseWriter = new HtmlResponseWriter();
			_container.Stub(c => c.GetInstance<IResponseWriter>(Arg<string>.Is.Anything)).Return(responseWriter);
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