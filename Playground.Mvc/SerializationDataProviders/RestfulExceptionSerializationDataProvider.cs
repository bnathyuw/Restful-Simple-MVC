using System;
using System.Xml.Linq;
using RestfulSimpleMvc.Core.Exceptions;

namespace RestfulSimpleMvc.Core.SerializationDataProviders
{
	public class RestfulExceptionSerializationDataProvider: DefaultSerializationDataProvider<RestfulException>
	{
		protected override dynamic GetJsonData(RestfulException content) {
			return new { content.HttpStatusCode, content.Message };
		}

		protected override XDocument GetXmlData(RestfulException content) {
			return new XDocument(new XElement("exception",
				new XElement("status",
					new XAttribute("code", (Int32)content.HttpStatusCode),
					content.HttpStatusCode),
				new XElement("message", content.Message),
				new XElement("stack-trace", 
					new XCData(content.StackTrace))));
		}
	}
}