using System.Net;
using NUnit.Framework;

namespace RestfulSimpleMvc.Smoke.Tests
{
	[TestFixture]
	public class ExceptionTests
	{
		[Test]
		public void CanServeException()
		{
			var webException = Assert.Throws<WebException>(() => WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/Exceptions/404"));
			var response = webException.Response as HttpWebResponse;
			Assert.That(response != null);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound)); Assert.That(response.ContentType, Is.StringStarting("text/xml"));
		}

		[Test]
		public void CanServeHtmlWithFormatSuffix()
		{
			var webException = Assert.Throws<WebException>(() => WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/Exceptions/404.html"));
			var response = webException.Response as HttpWebResponse;
			Assert.That(response != null);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound)); Assert.That(response.ContentType, Is.StringStarting("text/html"));
		}

		[Test]
		public void CanServeHtmlWithAcceptHeader()
		{
			var webException = Assert.Throws<WebException>(() => WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/Exceptions/404", "text/html"));
			var response = webException.Response as HttpWebResponse;
			Assert.That(response != null);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound)); Assert.That(response.ContentType, Is.StringStarting("text/html"));
		}

		[Test]
		public void CanServeJsonWithFormatSuffix()
		{
			var webException = Assert.Throws<WebException>(() => WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/Exceptions/404.json"));
			var response = webException.Response as HttpWebResponse;
			Assert.That(response != null);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound)); Assert.That(response.ContentType, Is.StringStarting("application/json"));
		}

		[Test]
		public void CanServeJsonWithAcceptHeader()
		{
			var webException = Assert.Throws<WebException>(() => WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/Exceptions/404", "application/json"));
			var response = webException.Response as HttpWebResponse;
			Assert.That(response != null);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound)); Assert.That(response.ContentType, Is.StringStarting("application/json"));
		}

		[Test]
		public void CanServeXmlWithFormatSuffix()
		{
			var webException = Assert.Throws<WebException>(() => WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/Exceptions/404.xml"));
			var response = webException.Response as HttpWebResponse;
			Assert.That(response != null);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound)); Assert.That(response.ContentType, Is.StringStarting("text/xml"));
		}

		[Test]
		public void CanServeXmlWithAcceptHeader()
		{
			var webException = Assert.Throws<WebException>(() => WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/Exceptions/404", "text/xml"));
			var response = webException.Response as HttpWebResponse;
			Assert.That(response != null);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound)); Assert.That(response.ContentType, Is.StringStarting("text/xml"));
		}
	}
}