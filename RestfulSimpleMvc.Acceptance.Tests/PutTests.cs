using System.Net;
using NUnit.Framework;

namespace RestfulSimpleMvc.Acceptance.Tests
{
	[TestFixture]
	public class PutTests
	{
		private const string HOST = "http://localhost";
		private const string URL = "/restful-simple-mvc/posts/123";

		[Test]
		public void Put_with_xml_response_type_returns_correct_response_code() {
			var response = WebRequester.Put(HOST + URL, "id=123", acceptHeader: "application/xml");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Put_with_json_response_type_returns_correct_response_code() {
			var response = WebRequester.Put(HOST + URL, "id=123", acceptHeader: "application/json");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Put_with_html_response_type_returns_correct_response_code() {
			var response = WebRequester.Put(HOST + URL, "id=123", acceptHeader: "text/html");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Put_with_jsonp_response_type_returns_correct_response_code() {
			var response = WebRequester.Put(HOST + URL, "callback=callback&id=123", acceptHeader: "application/json-p");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Put_returns_moved_permanently_code_if_resource_location_has_changed() {
			var response = WebRequester.Put(HOST + URL, "id=456", acceptHeader : "application/xml");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.MovedPermanently));
		}

		[Test]
		public void Put_returns_correct_location_if_resource_location_has_changed() {
			var response = WebRequester.Put(HOST + URL, "id=456", acceptHeader: "application/xml");
			var location = response.Headers["location"].ToLowerInvariant();
			Assert.That(location, Is.StringContaining("/restful-simple-mvc/posts/456"));
		}
	}
}