using NUnit.Framework;

namespace RestfulSimpleMvc.Acceptance.Tests
{
	[TestFixture]
	public class ContentTests
	{
		[Test]
		public void CanServeContent() {
			var response = WebRequester.Get("http://localhost/restful-simple-mvc/");
			Assert.That(response.ContentType, Is.StringStarting("text/xml"));
		}

		[Test]
		public void CanServeHtmlWithFormatSuffix()
		{
			var response = WebRequester.Get("http://localhost/restful-simple-mvc/.html");
			Assert.That(response.ContentType, Is.StringStarting("text/html"));
		}

		[Test]
		public void CanServeHtmlWithAcceptHeader()
		{
			var response = WebRequester.Get("http://localhost/restful-simple-mvc/", "text/html");
			Assert.That(response.ContentType, Is.StringStarting("text/html"));
		}

		[Test]
		public void CanServeJsonWithFormatSuffix() {
			var response = WebRequester.Get("http://localhost/restful-simple-mvc/.json");
			Assert.That(response.ContentType, Is.StringStarting("application/json"));
		}

		[Test]
		public void CanServeJsonWithAcceptHeader() {
			var response = WebRequester.Get("http://localhost/restful-simple-mvc/", "application/json");
			Assert.That(response.ContentType, Is.StringStarting("application/json"));
		}

		[Test]
		public void CanServeXmlWithFormatSuffix()
		{
			var response = WebRequester.Get("http://localhost/restful-simple-mvc/.xml");
			Assert.That(response.ContentType, Is.StringStarting("text/xml"));
		}

		[Test]
		public void CanServeXmlWithAcceptHeader()
		{
			var response = WebRequester.Get("http://localhost/restful-simple-mvc/", "text/xml");
			Assert.That(response.ContentType, Is.StringStarting("text/xml"));
		}
	}
}
