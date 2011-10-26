using System.Net;
using NUnit.Framework;

namespace RestfulSimpleMvc.Acceptance.Tests
{
	[TestFixture]
	public class ExceptionTests
	{
		[Test]
		public void CanServeException()
		{
			var response = WebRequester.Get("http://localhost/restful-simple-mvc/Exceptions/404");
			Assert.That(response != null);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound)); Assert.That(response.ContentType, Is.StringStarting("text/xml"));
		}

		[Test]
		public void CanServeHtmlWithFormatSuffix()
		{
			var response =  WebRequester.Get("http://localhost/restful-simple-mvc/Exceptions/404.html");
			Assert.That(response != null);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound)); Assert.That(response.ContentType, Is.StringStarting("text/html"));
		}

		[Test]
		public void CanServeHtmlWithAcceptHeader()
		{
			var response =  WebRequester.Get("http://localhost/restful-simple-mvc/Exceptions/404", "text/html");
			Assert.That(response != null);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound)); Assert.That(response.ContentType, Is.StringStarting("text/html"));
		}

		[Test]
		public void CanServeJsonWithFormatSuffix()
		{
			var response = WebRequester.Get("http://localhost/restful-simple-mvc/Exceptions/404.json");
			Assert.That(response != null);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound)); Assert.That(response.ContentType, Is.StringStarting("application/json"));
		}

		[Test]
		public void CanServeJsonWithAcceptHeader()
		{
			var response =  WebRequester.Get("http://localhost/restful-simple-mvc/Exceptions/404", "application/json");
			Assert.That(response != null);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound)); Assert.That(response.ContentType, Is.StringStarting("application/json"));
		}

		[Test]
		public void CanServeXmlWithFormatSuffix()
		{
			var response = WebRequester.Get("http://localhost/restful-simple-mvc/Exceptions/404.xml");
			Assert.That(response != null);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound)); Assert.That(response.ContentType, Is.StringStarting("text/xml"));
		}

		[Test]
		public void CanServeXmlWithAcceptHeader()
		{
			var response =  WebRequester.Get("http://localhost/restful-simple-mvc/Exceptions/404", "text/xml");
			Assert.That(response != null);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound)); Assert.That(response.ContentType, Is.StringStarting("text/xml"));
		}
	}
}