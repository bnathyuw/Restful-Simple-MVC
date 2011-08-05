using System;
using System.Xml.Linq;
using RestfulSimpleMvc.Core.Exceptions;
using RestfulSimpleMvc.Core.StatusCodes;

namespace RestfulSimpleMvc.Core.Serialization
{
	public class RestfulExceptionSerializationDataProvider: SerializationDataProvider<RestfulException>
	{
		protected override dynamic GetJsonData(RestfulException content) {
			return SerializeExceptionToJson(content);
		}

		private static dynamic SerializeExceptionToJson(Exception content) {
			var statusCoded = content as IStatusCoded;
			if (statusCoded != null) return new {statusCoded.HttpStatusCode, content.Message, content.StackTrace, InnerException = content.InnerException == null ? null : SerializeExceptionToJson(content.InnerException)};
			return new { content.Message, content.StackTrace, InnerException = content.InnerException == null ? null : SerializeExceptionToJson(content.InnerException) };
		}

		protected override XDocument GetXmlData(RestfulException content) {
			return new XDocument(SerializeExceptionToXml(content));
		}

		private static XElement SerializeExceptionToXml(Exception content) {
			return new XElement("exception",
								SerializeStatusCodeToXml(content),
								new XElement("message", content.Message),
								content.StackTrace == null
									? null
									: new XElement("stack-trace",
												   new XCData(content.StackTrace)),
								content.InnerException == null
									? null
									: new XElement("inner-exception", SerializeExceptionToXml(content.InnerException)));

		}

		private static XElement SerializeStatusCodeToXml(Exception content) {
			var statusCoded = content as IStatusCoded;
			if (statusCoded == null) return null;
			return new XElement("status",
				new XAttribute("code",
					(Int32)statusCoded.HttpStatusCode),
					statusCoded.HttpStatusCode);
		}
	}
}