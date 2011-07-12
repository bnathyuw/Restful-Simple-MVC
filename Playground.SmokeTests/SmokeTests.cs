using System.Net;
using NUnit.Framework;

namespace Playground.SmokeTests
{
	[TestFixture]
	public class SmokeTests
	{
		[Test]
		public void CanServeContent() {
			WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/");
		}

		[Test]
		public void CanServeWithFormatSuffix() {
			var response = WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/.json");
			Assert.That(response.ContentType, Is.StringStarting("application/json"));
		}

		[Test]
		public void CanServeWithAcceptHeader() {
			var response = WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/", "application/json");
			Assert.That(response.ContentType, Is.StringStarting("application/json"));
		}

		[Test]
		public void CanServeException() {
			var webException = Assert.Throws<WebException>(() => WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/Exceptions/404", "text/html"));
			var httpWebResponse = webException.Response as HttpWebResponse;
			Assert.That(httpWebResponse != null);
			Assert.That(httpWebResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
		}
	}
}
