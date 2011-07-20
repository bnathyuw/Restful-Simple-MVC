using NUnit.Framework;

namespace RestfulSimpleMvc.Smoke.Tests
{
	[TestFixture]
	public class ContentTests
	{
		[Test]
		public void CanServeContent() {
			var response = WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/");
			Assert.That(response.ContentType, Is.StringStarting("text/xml"));
		}

		[Test]
		public void CanServeHtmlWithFormatSuffix()
		{
			var response = WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/.html");
			Assert.That(response.ContentType, Is.StringStarting("text/html"));
		}

		[Test]
		public void CanServeHtmlWithAcceptHeader()
		{
			var response = WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/", "text/html");
			Assert.That(response.ContentType, Is.StringStarting("text/html"));
		}

		[Test]
		public void CanServeJsonWithFormatSuffix() {
			var response = WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/.json");
			Assert.That(response.ContentType, Is.StringStarting("application/json"));
		}

		[Test]
		public void CanServeJsonWithAcceptHeader() {
			var response = WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/", "application/json");
			Assert.That(response.ContentType, Is.StringStarting("application/json"));
		}

		[Test]
		public void CanServeXmlWithFormatSuffix()
		{
			var response = WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/.xml");
			Assert.That(response.ContentType, Is.StringStarting("text/xml"));
		}

		[Test]
		public void CanServeXmlWithAcceptHeader()
		{
			var response = WebRequester.MakeGetRequest("http://localhost/restful-simple-mvc/", "text/xml");
			Assert.That(response.ContentType, Is.StringStarting("text/xml"));
		}
	}
}
