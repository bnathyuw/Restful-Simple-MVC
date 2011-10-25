using System;
using System.IO;
using System.Net;
using NUnit.Framework;

namespace RestfulSimpleMvc.Acceptance.Tests
{
	public static class WebRequester{
		public static HttpWebResponse MakeGetRequest(string url, string acceptHeader = null) {
			var request = WebRequest.Create(url);

			AddAcceptHeader(request, acceptHeader);

			LogRequest(request);

			try
			{
				var response = request.GetResponse();

				Assert.That(response != null);

				LogResponse(response);

				return response as HttpWebResponse;
			}
			catch (WebException ex) {
				var response = ex.Response;

				Assert.That(response != null);

				LogResponse(response);

				throw;
			}
		}

		private static void AddAcceptHeader(WebRequest request, string acceptHeader) {
			var webRequest = request as HttpWebRequest;
			Assert.That(webRequest != null, "Accept header cannot be added to request. Aborting.");
			if (!string.IsNullOrWhiteSpace(acceptHeader)) webRequest.Accept = acceptHeader;
		}

		private static void LogRequest(WebRequest request) {
			Console.WriteLine("Request\n{0} {1}", request.Method, request.RequestUri);
			foreach (var h in request.Headers.AllKeys) {
				Console.WriteLine("{0}: {1}", h, request.Headers[h]);
			}
		}

		private static void LogResponse(WebResponse response) {
			Console.WriteLine("\nResponse");
			foreach (var h in response.Headers.AllKeys) {
				Console.WriteLine("{0}: {1}", h, response.Headers[h]);
			}

			var responseStream = response.GetResponseStream();
			if (responseStream == null) return;

			var streamReader = new StreamReader(responseStream);
			var responseString = streamReader.ReadToEnd();
			Console.WriteLine("Content:\n{0}", responseString);
		}
	}
}