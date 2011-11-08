using System.Net;
using NUnit.Framework;

namespace RestfulSimpleMvc.Acceptance.Tests
{
	[TestFixture]
	public class PostTests
	{
		private const string HOST = "http://localhost";
		private const string URL = "/restful-simple-mvc/posts";

		[Test]
		public void Post_with_xml_response_type_returns_correct_response_code() {
			var response = WebRequester.Post(HOST + URL, "", acceptHeader: "application/xml");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
		}

		[Test]
		public void Post_with_json_response_type_returns_correct_response_code() {
			var response = WebRequester.Post(HOST + URL, "", acceptHeader: "application/json");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
		}

		[Test]
		public void Post_with_html_response_type_returns_correct_response_code() {
			var response = WebRequester.Post(HOST + URL, "", acceptHeader: "text/html");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.MovedPermanently));
		}

		[Test]
		public void Post_with_jsonp_response_type_returns_correct_response_code() {
			var response = WebRequester.Post(HOST + URL, "callback=callback", acceptHeader: "application/json-p");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
		}

		[Test]
		public void Post_with_xml_response_type_returns_location_of_resource_in_location_header() {
			var response = WebRequester.Post(HOST + URL, "", acceptHeader: "application/xml");
			var location = response.Headers["location"];
			Assert.That(location.ToLowerInvariant(), Is.StringMatching(URL + "/\\d*"));
		}

		[Test]
		public void Post_with_json_response_type_returns_location_of_resource_in_location_header() {
			var response = WebRequester.Post(HOST + URL, "", acceptHeader: "application/json");
			var location = response.Headers["location"];
			Assert.That(location.ToLowerInvariant(), Is.StringMatching(URL + "/\\d*"));
		}

		[Test]
		public void Post_with_jsonp_response_type_returns_location_of_resource_in_location_header() {
			var response = WebRequester.Post(HOST + URL, "callback=callback", acceptHeader: "application/json-p");
			var location = response.Headers["location"];
			Assert.That(location.ToLowerInvariant(), Is.StringMatching(URL + "/\\d*"));
		}

		[Test]
		public void Post_with_html_response_type_returns_location_of_resource_in_location_header() {
			var response = WebRequester.Post(HOST + URL, "", acceptHeader: "text/html");
			var location = response.Headers["location"];
			Assert.That(location.ToLowerInvariant(), Is.StringMatching(URL + "/.*"));
		}
	}
}