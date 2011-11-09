using System.Net;
using System.Web.Mvc;
using NUnit.Framework;
using RestfulSimpleMvc.Core.Location;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.Results;
using RestfulSimpleMvc.Core.StatusCodes;
using RestfulSimpleMvc.Web.Models;
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
			_restfulResult = new RestfulResult(responseWriter, _content, null, null, null, null, null);
			_restfulResult.ExecuteResult(null);
			responseWriter.AssertWasCalled(rw => rw.WriteResponse(null, _content, null));
		}

		[Test]
		public void Execute_result_looks_up_created_status_code_if_view_name_is_post() {
			var statusCodeTranslator = MockRepository.GenerateStub<IStatusCodeTranslator>();
			var responseUpdater = MockRepository.GenerateStub<IResponseUpdater>();
			var locationProvider = MockRepository.GenerateStub<ILocationProvider>();
			_restfulResult = new RestfulResult(null, _content, "POST", responseUpdater, statusCodeTranslator, locationProvider, null);
			_restfulResult.ExecuteResult(null);
			statusCodeTranslator.AssertWasCalled(t => t.LookUp(HttpStatusCode.Created));
		}

		[Test]
		public void Execute_result_looks_up_location_if_view_name_is_post() {
			var statusCodeTranslator = MockRepository.GenerateStub<IStatusCodeTranslator>();
			var responseUpdater = MockRepository.GenerateStub<IResponseUpdater>();
			var locationProvider = MockRepository.GenerateStub<ILocationProvider>();
			_restfulResult = new RestfulResult(null, _content, "POST", responseUpdater, statusCodeTranslator, locationProvider, null);
			_restfulResult.ExecuteResult(null);
			locationProvider.AssertWasCalled(p => p.GetLocation(_content, null));
		}

		[Test]
		public void Execute_result_looks_up_location_if_view_name_is_put() {
			var statusCodeTranslator = MockRepository.GenerateStub<IStatusCodeTranslator>();
			var responseUpdater = MockRepository.GenerateStub<IResponseUpdater>();
			var locationProvider = MockRepository.GenerateStub<ILocationProvider>();
			const string location = "abc";
			locationProvider.Stub(p => p.GetLocation(Arg<object>.Is.Anything, Arg<ControllerContext>.Is.Anything)).Return(location);
			var contextHelper = MockRepository.GenerateStub<IContextHelper>();
			contextHelper.Stub(h => h.GetRequestLocation(Arg<ControllerContext>.Is.Anything)).Return(location);
			_restfulResult = new RestfulResult(null, _content, "PUT", responseUpdater, statusCodeTranslator, locationProvider, contextHelper);
			_restfulResult.ExecuteResult(null);
			locationProvider.AssertWasCalled(p => p.GetLocation(_content, null));
		}
	}
}