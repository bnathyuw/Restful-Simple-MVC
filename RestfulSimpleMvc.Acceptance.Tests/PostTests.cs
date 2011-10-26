using System.Net;
using NUnit.Framework;

namespace RestfulSimpleMvc.Acceptance.Tests
{
	[TestFixture]
	public class PostTests
	{
		[Test]
		public void Post_with_xml_response_type_returns_correct_response_code() {
			var response = WebRequester.Post("http://localhost/restful-simple-mvc/posts", "", acceptHeader: "application/xml");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
		}

		[Test]
		public void Post_with_json_response_type_returns_correct_response_code() {
			var response = WebRequester.Post("http://localhost/restful-simple-mvc/posts", "", acceptHeader: "application/json");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
		}

		[Test]
		public void Post_with_html_response_type_returns_correct_response_code() {
			var response = WebRequester.Post("http://localhost/restful-simple-mvc/posts", "", acceptHeader: "text/html");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.MovedPermanently));
		}

		[Test]
		public void Post_with_jsonp_response_type_returns_correct_response_code() {
			var response = WebRequester.Post("http://localhost/restful-simple-mvc/posts", "callback=callback", acceptHeader: "application/json-p");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
		}
		 
	}
}