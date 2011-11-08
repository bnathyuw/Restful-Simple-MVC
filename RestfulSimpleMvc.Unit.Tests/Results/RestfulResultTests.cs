using System.Net;
using NUnit.Framework;
using RestfulSimpleMvc.Core.Location;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.Results;
using RestfulSimpleMvc.Core.StatusCodes;
using Rhino.Mocks;

namespace RestfulSimpleMvc.Unit.Tests.Results
{
	[TestFixture]
	public class RestfulResultTests
	{
		private RestfulResult _restfulResult;
		private object _content;

		[SetUp]
		public void SetUp() {
			_content = new {};
		}

		[Test]
		public void ExecuteResultCallsWriteResponseCorrectly() {
			var responseWriter = MockRepository.GenerateStub<IResponseWriter>();
			_restfulResult = new RestfulResult(responseWriter, _content, null, null, null, null);
			_restfulResult.ExecuteResult(null);
			responseWriter.AssertWasCalled(rw => rw.WriteResponse(null, _content, null));
		}

		[Test]
		public void Execute_result_looks_up_created_status_code_if_view_name_is_post() {
			var statusCodeTranslator = MockRepository.GenerateStub<IStatusCodeTranslator>();
			var responseUpdater = MockRepository.GenerateStub<IResponseUpdater>();
			ILocationProvider locationProvider = MockRepository.GenerateStub<ILocationProvider>();
			_restfulResult = new RestfulResult(null, _content, "POST", responseUpdater, statusCodeTranslator, locationProvider);
			_restfulResult.ExecuteResult(null);
			statusCodeTranslator.AssertWasCalled(t => t.LookUp(HttpStatusCode.Created));

		}
	}
}