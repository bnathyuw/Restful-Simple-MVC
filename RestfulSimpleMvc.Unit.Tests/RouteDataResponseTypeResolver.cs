using NUnit.Framework;
using RestfulSimpleMvc.Core;

namespace RestfulSimpleMvc.Unit.Tests
{
	[TestFixture]
	public class RouteDataResponseTypeResolverTests
	{
		private RouteDataResponseTypeResolver _resolver;

		[SetUp]
		public void SetUp() {
			_resolver = new RouteDataResponseTypeResolver();
		}

		[Test]
		public void HtmlReturnsHtml() {
			var responseType = _resolver.Resolve("html");
			Assert.That(responseType, Is.EqualTo(ResponseType.Html));
		}

		[Test]
		public void XmlReturnsXml() {
			var responseType = _resolver.Resolve("xml");
			Assert.That(responseType, Is.EqualTo(ResponseType.Xml));
		}

		[Test]
		public void JsonReturnsJson()
		{
			var responseType = _resolver.Resolve("json");
			Assert.That(responseType, Is.EqualTo(ResponseType.Json));
		}

		[Test]
		public void CrapReturnsNull()
		{
			var responseType = _resolver.Resolve("crap");
			Assert.That(responseType, Is.Null);
		}

		[Test]
		public void NullReturnsNull()
		{
			var responseType = _resolver.Resolve(null);
			Assert.That(responseType, Is.Null);
		}

		[Test]
		public void EmptyReturnsNull()
		{
			var responseType = _resolver.Resolve("");
			Assert.That(responseType, Is.Null);
		}
	}
}