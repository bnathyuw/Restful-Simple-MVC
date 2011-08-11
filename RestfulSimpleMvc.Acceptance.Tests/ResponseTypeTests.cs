using System.Net;
using NUnit.Framework;
using WebRequestWrapper;

namespace RestfulSimpleMvc.Acceptance.Tests
{
	[TestFixture]
	public class ResponseTypeTests
	{
		private readonly WebRequester _webRequester = new WebRequester(new ConsoleRequestLogger());

		[Test]
		public void IfNoCallbackIsSpecifiedWithJsonPInQueryThenReturnBadRequest() {
			var response = _webRequester.Get("http://localhost/restful-simple-mvc/.jsonp");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

		[Test]
		public void IfCallbackIsSpecifiedWithJsonPInQueryThenReturnOK() {
			var response = _webRequester.Get("http://localhost/restful-simple-mvc/.jsonp?callback=callback");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}
	}
}