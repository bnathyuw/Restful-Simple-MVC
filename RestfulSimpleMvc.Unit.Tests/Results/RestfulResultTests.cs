using System.Net;
using System.Web.Mvc;
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
		private IResponseWriter _responseWriter;
		private IStatusCodeTranslator _statusCodeTranslator;
		private IResponseUpdater _responseUpdater;
		private ILocationProvider _locationProvider;
		private IContextHelper _contextHelper;

		[SetUp]
		public void SetUp() {
			_responseUpdater = MockRepository.GenerateStub<IResponseUpdater>();
			_responseWriter = MockRepository.GenerateStub<IResponseWriter>();
			_statusCodeTranslator = MockRepository.GenerateStub<IStatusCodeTranslator>();
			_locationProvider = MockRepository.GenerateStub<ILocationProvider>();
			_contextHelper = MockRepository.GenerateStub<IContextHelper>();
			_content = new {};
		}

		[Test]
		public void Execute_result_calls_write_response_correctly() {
			_restfulResult = new RestfulResult(_responseWriter, _content, null, null, null, null, null);
			
			_restfulResult.ExecuteResult(null);
			
			_responseWriter.AssertWasCalled(rw => rw.WriteResponse(null, _content, null));
		}

		[Test]
		public void Execute_result_gets_no_content_status_code_if_content_is_null() {
			_restfulResult = new RestfulResult(_responseWriter, null, null, _responseUpdater, _statusCodeTranslator, null, null);
			_statusCodeTranslator.Stub(t => t.LookUp(Arg<ResourceStatus>.Is.Anything)).Return(HttpStatusCode.NoContent);
			
			_restfulResult.ExecuteResult(null);

			_statusCodeTranslator.AssertWasCalled(t => t.LookUp(ResourceStatus.Deleted));
		}

		[Test]
		public void Execute_restult_sets_code_from_translator_when_content_is_null() {
			_restfulResult = new RestfulResult(_responseWriter, null, null, _responseUpdater, _statusCodeTranslator, null, null);
			_statusCodeTranslator.Stub(t => t.LookUp(Arg<ResourceStatus>.Is.Anything)).Return(HttpStatusCode.NonAuthoritativeInformation);

			_restfulResult.ExecuteResult(null);

			_responseUpdater.AssertWasCalled(u => u.SetStatusCode(null, HttpStatusCode.NonAuthoritativeInformation));
		}

		[Test]
		public void Execute_result_looks_up_created_status_code_if_view_name_is_post() {
			_restfulResult = new RestfulResult(null, _content, "POST", _responseUpdater, _statusCodeTranslator, _locationProvider, null);
			
			_restfulResult.ExecuteResult(null);

			_statusCodeTranslator.AssertWasCalled(t => t.LookUp(ResourceStatus.Created));
		}

		[Test]
		public void Execute_result_looks_up_location_if_view_name_is_post() {
			_restfulResult = new RestfulResult(null, _content, "POST", _responseUpdater, _statusCodeTranslator, _locationProvider, null);
			
			_restfulResult.ExecuteResult(null);
			
			_locationProvider.AssertWasCalled(p => p.GetLocation(_content, null));
		}

		[Test]
		public void Execute_result_looks_up_location_if_view_name_is_put() {
			const string location = "abc";
			_locationProvider.Stub(p => p.GetLocation(Arg<object>.Is.Anything, Arg<ControllerContext>.Is.Anything)).Return(location);
			_contextHelper.Stub(h => h.GetRequestLocation(Arg<ControllerContext>.Is.Anything)).Return(location);
			_restfulResult = new RestfulResult(null, _content, "PUT", _responseUpdater, _statusCodeTranslator, _locationProvider, _contextHelper);
			
			_restfulResult.ExecuteResult(null);
			
			_locationProvider.AssertWasCalled(p => p.GetLocation(_content, null));
		}
	}
}