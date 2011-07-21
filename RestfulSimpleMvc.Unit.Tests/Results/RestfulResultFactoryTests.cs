using NUnit.Framework;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.Results;
using Rhino.Mocks;

namespace RestfulSimpleMvc.Unit.Tests.Results
{
	[TestFixture]
	public class RestfulResultFactoryTests
	{
		private RestfulResultFactory _factory;

		[SetUp]
		public void SetUp() {
			_factory = new RestfulResultFactory();
		}

		[Test]
		public void BuildReturnsRestfulResponse() {
			var responseWriter = MockRepository.GenerateStub<IResponseWriter>();
			var response = _factory.Build(responseWriter, null, null, null);
			Assert.That(response,Is.Not.Null);
		}
	}
}