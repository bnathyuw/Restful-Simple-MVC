using System.Web;
using System.Web.Mvc;
using NUnit.Framework;
using Playground.Mvc;
using Playground.Mvc.ResponseWriters;
using Rhino.Mocks;

namespace Playground.Unit.Tests.Mvc
{
	[TestFixture]
	public class RestfulResultTests
	{
		private const string RESPONSE = "response";
		private ControllerContext _controllerContext;
		private HttpContextBase _httpContext;
		private HttpResponseBase _httpResponse;
		private RestfulResult _restfulResult;
		private IResponseWriter _responseWriter;
		private object _content;

		[SetUp]
		public void SetUp() {
			_controllerContext = MockRepository.GenerateStub<ControllerContext>();
			_httpContext = MockRepository.GenerateStub<HttpContextBase>();
			_controllerContext.HttpContext = _httpContext;
			_httpResponse = MockRepository.GenerateStub<HttpResponseBase>();
			_httpContext.Stub(c => c.Response).Return(_httpResponse); 
			_responseWriter = MockRepository.GenerateStub<IResponseWriter>();
			_content = new {};
			_restfulResult = new RestfulResult(_responseWriter, _content);
		}

		[Test]
		public void ExecuteResultSetsContentFromContentWriter() {
			_restfulResult.ExecuteResult(_controllerContext);
			_responseWriter.AssertWasCalled(rw => rw.WriteResponse(_httpResponse, _content));
		}
	}
}