using System.Net;
using NUnit.Framework;

namespace RestfulSimpleMvc.Acceptance.Tests
{
	[TestFixture]
	public class DeleteTests
	{
		private const string HOST = "http://localhost";
		private const string URL = "/restful-simple-mvc/posts/123";

		[Test]
		public void Delete_with_xml_response_type_returns_correct_response_code() {
			var response = WebRequester.Delete(HOST + URL, acceptHeader: "application/xml");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
		}

		[Test]
		public void Delete_with_json_response_type_returns_correct_response_code() {
			var response = WebRequester.Delete(HOST + URL, acceptHeader: "application/json");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
		}

		[Test]
		public void Delete_with_html_response_type_returns_correct_response_code() {
			var response = WebRequester.Delete(HOST + URL, acceptHeader: "text/html");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
		}

		[Test]
		public void Delete_with_jsonp_response_type_returns_correct_response_code() {
			var response = WebRequester.Delete(HOST + URL + "?callback=callback", acceptHeader: "application/json-p");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
		}
	}
}