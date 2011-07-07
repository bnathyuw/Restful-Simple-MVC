using System.Net;
using NUnit.Framework;

namespace Playground.SmokeTests
{
	[TestFixture]
	public class SmokeTests
	{
		[Test]
		public void CanServeContent() {
			var request = WebRequest.Create("http://localhost/restful-simple-mvc/");
			var response = request.GetResponse();
			Assert.That(response, Is.Not.Null);
		}

		[Test]
		public void CanServeWithFormatSuffix()
		{
			var request = WebRequest.Create("http://localhost/restful-simple-mvc/.json");
			var response = request.GetResponse();
			Assert.That(response != null);
			Assert.That(response.ContentType, Is.StringStarting("application/json"));
		}

		[Test]
		public void CanServeWithAcceptHeader()
		{
			var request = WebRequest.Create("http://localhost/restful-simple-mvc/") as HttpWebRequest;
			Assert.That(request != null);
			request.Accept = "application/json";
			var response = request.GetResponse();
			Assert.That(response != null);
			Assert.That(response.ContentType, Is.StringStarting("application/json"));
		}
	}
}
