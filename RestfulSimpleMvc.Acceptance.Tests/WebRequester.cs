using System;
using System.IO;
using System.Net;
using System.Text;
using NUnit.Framework;

namespace RestfulSimpleMvc.Acceptance.Tests
{
	public static class WebRequester{
		public static HttpWebResponse Get(string url, string acceptHeader = null) {
			return DoRequest(url, acceptHeader, "GET", null, null);
		}

		public static HttpWebResponse Post(string url, string data, string contentType = "application/x-www-form-urlencoded", string acceptHeader = null) {
			return DoRequest(url, acceptHeader, "POST", data, contentType);
		}
		
		private static HttpWebResponse DoRequest(string url, string acceptHeader, string method, string data, string contentType) {
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = method;
			request.AllowAutoRedirect = false;

			if (data != null) {
				var requestStream = request.GetRequestStream();
				var bytes = Encoding.UTF8.GetBytes(data);
				requestStream.Write(bytes, 0, bytes.Length);
				request.ContentType = contentType;
			}

			AddAcceptHeader(request, acceptHeader);

			LogRequest(request);

			try {
				var response = request.GetResponse();

				Assert.That(response != null);

				LogResponse(response);

				return response as HttpWebResponse;
			} catch (WebException ex) {
				var response = ex.Response;

				Assert.That(response != null);

				LogResponse(response);

				return response as HttpWebResponse;
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