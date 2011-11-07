using System.Net;
using NUnit.Framework;

namespace RestfulSimpleMvc.Acceptance.Tests
{
	[TestFixture]
	public class ResponseTypeTests
	{
		[Test]
		public void If_no_callback_is_specified_with_jsonp_in_query_then_return_bad_request() {
			var response = WebRequester.Get("http://localhost/restful-simple-mvc/.jsonp");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

		[Test]
		public void If_callback_is_specified_with_jsonp_in_query_then_return_ok() {
			var response = WebRequester.Get("http://localhost/restful-simple-mvc/.jsonp?callback=callback");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}
	}
}