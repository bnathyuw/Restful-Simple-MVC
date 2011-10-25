using System.Net;
using NUnit.Framework;
using WebRequestWrapper;

namespace RestfulSimpleMvc.Acceptance.Tests
{
	[TestFixture]
	public class ResponseTypeTests
	{
		[Test]
		public void IfNoCallbackIsSpecifiedWithJsonPInQueryThenReturnBadRequest() {
			var response = WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/.jsonp");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

		[Test]
		public void IfCallbackIsSpecifiedWithJsonPInQueryThenReturnOK() {
			var response = WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/.jsonp?callback=callback");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}
	}
}