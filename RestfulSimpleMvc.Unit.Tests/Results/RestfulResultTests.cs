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
		private IResponseWriter _responseWriter;
		private object _content;
		private IStatusCodeWriter _statusCodeWriter;

		[SetUp]
		public void SetUp() {
			_controllerContext = MockRepository.GenerateStub<ControllerContext>();
			_httpContext = MockRepository.GenerateStub<HttpContextBase>();
			_controllerContext.HttpContext = _httpContext;
			_httpResponse = MockRepository.GenerateStub<HttpResponseBase>();
			_httpContext.Stub(c => c.Response).Return(_httpResponse); 
			_responseWriter = MockRepository.GenerateStub<IResponseWriter>();
			_content = new {};
			_statusCodeWriter = MockRepository.GenerateStub<IStatusCodeWriter>();
			_restfulResult = new RestfulResult(_responseWriter, _content, null, null);
		}

		[Test]
		public void ExecuteResultCallsWriteResponseCorrectly() {
			_restfulResult.ExecuteResult(_controllerContext);
			_responseWriter.AssertWasCalled(rw => rw.WriteResponse(_controllerContext, _content, null));
		}
	}
}