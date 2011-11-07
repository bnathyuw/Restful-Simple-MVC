using System.Net;
using System.Web;
using System.Web.Mvc;
using NUnit.Framework;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.Results;
using RestfulSimpleMvc.Core.StatusCodes;
using Rhino.Mocks;

namespace RestfulSimpleMvc.Unit.Tests.Results
{
	[TestFixture]
	public class RestfulResultTests
	{
		private ControllerContext _controllerContext;
		private HttpContextBase _httpContext;
		private HttpResponseBase _httpResponse;
		private RestfulResult _restfulResult;
		private object _content;

		[SetUp]
		public void SetUp() {
			_controllerContext = MockRepository.GenerateStub<ControllerContext>();
			_httpContext = MockRepository.GenerateStub<HttpContextBase>();
			_controllerContext.HttpContext = _httpContext;
			_httpResponse = MockRepository.GenerateStub<HttpResponseBase>();
			_httpContext.Stub(c => c.Response).Return(_httpResponse); 
			_content = new {};
		}

		[Test]
		public void ExecuteResultCallsWriteResponseCorrectly() {
			var responseWriter = MockRepository.GenerateStub<IResponseWriter>();
			_restfulResult = new RestfulResult(responseWriter, _content, null, null, null);
			_restfulResult.ExecuteResult(_controllerContext);
			responseWriter.AssertWasCalled(rw => rw.WriteResponse(_controllerContext, _content, null));
		}

		[Test]
		public void Execute_result_looks_up_created_status_code_if_view_name_is_post() {
			var statusCodeTranslator = MockRepository.GenerateStub<IStatusCodeTranslator>();
			var responseUpdater = MockRepository.GenerateStub<IResponseUpdater>();
			_restfulResult = new RestfulResult(null, _content, "POST", responseUpdater, statusCodeTranslator);
			_restfulResult.ExecuteResult(_controllerContext);
			statusCodeTranslator.AssertWasCalled(t => t.LookUp(HttpStatusCode.Created));

		}
	}
}