using NUnit.Framework;
using Playground.Mvc;
using Playground.Mvc.ResponseWriters;

namespace Playground.Unit.Tests.Mvc.ResponseWriters
{
	[TestFixture]
	public class ResponseWriterFactoryTests
	{
		private ResponseWriterFactory _responseWriterFactory;

		[SetUp]
		public void SetUp() {
			_responseWriterFactory = new ResponseWriterFactory();
		}

		[Test]
		public void BuildForHtmlReturnsHtmlResponseWriter() {
			var result = _responseWriterFactory.Build(ResponseType.Html);

			Assert.That(result, Is.TypeOf<HtmlResponseWriter>());
		}

		[Test]
		public void BuildForHtmlReturnsXmlResponseWriter()
		{
			var result = _responseWriterFactory.Build(ResponseType.Xml);

			Assert.That(result, Is.TypeOf<XmlResponseWriter>());
		}

		[Test]
		public void BuildForJsonReturnsHtmlResponseWriter()
		{
			var result = _responseWriterFactory.Build(ResponseType.Json);

			Assert.That(result, Is.TypeOf<JsonResponseWriter>());
		}
	}
}