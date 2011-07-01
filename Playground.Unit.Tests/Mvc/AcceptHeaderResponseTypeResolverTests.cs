using NUnit.Framework;
using Playground.Web.Mvc;

namespace Playground.Unit.Tests.Mvc
{
	[TestFixture]
	public class AcceptHeaderResponseTypeResolverTests
	{
		private AcceptHeaderResponseTypeResolver _acceptHeaderResponseTypeResolver;

		[SetUp]
		public void SetUp() {
			IAcceptHeaderParser acceptHeaderParser = new AcceptHeaderParser();
			IEnumNameParser<ResponseType> enumNameParser = new EnumNameParser<ResponseType>();
			_acceptHeaderResponseTypeResolver = new AcceptHeaderResponseTypeResolver(acceptHeaderParser, enumNameParser);
		}

		[Test]
		public void GetsCorrectXmlResponseTypeFromAcceptHeaders()
		{
			var responseType = _acceptHeaderResponseTypeResolver.Resolve("text/xml");
			Assert.That(responseType, Is.EqualTo(ResponseType.Xml));
		}

		[Test]
		public void GetsCorrectHtmlResponseTypeFromAcceptHeaders()
		{
			var responseType = _acceptHeaderResponseTypeResolver.Resolve("text/html");
			Assert.That(responseType, Is.EqualTo(ResponseType.Html));
		}

		[Test]
		public void GetsCorrectJsonResponseTypeFromAcceptHeaders()
		{
			var responseType = _acceptHeaderResponseTypeResolver.Resolve("application/json");
			Assert.That(responseType, Is.EqualTo(ResponseType.Json));
		}

		[Test]
		public void GetsCorrectResponseTypeWhenHtmlAndXmlAccepted()
		{
			var responseType = _acceptHeaderResponseTypeResolver.Resolve("text/html,text/xml");
			Assert.That(responseType, Is.EqualTo(ResponseType.Html));
		}

		[Test]
		public void GetsCorrectResponseTypeWhenHtmlAndJsonAccepted()
		{
			var responseType = _acceptHeaderResponseTypeResolver.Resolve("text/html, application/json");
			Assert.That(responseType, Is.EqualTo(ResponseType.Html));
		}

		[Test]
		public void GetsCorrectResponseTypeWhenJsonAndXmlAccepted()
		{
			var responseType = _acceptHeaderResponseTypeResolver.Resolve("text/xml ,application/json");
			Assert.That(responseType, Is.EqualTo(ResponseType.Json));
		}

		[Test]
		public void GetsCorrectResponseTypeWhenHtmlAndJsonAndXmlAccepted()
		{
			var responseType = _acceptHeaderResponseTypeResolver.Resolve("text/xml, text/html ,application/json");
			Assert.That(responseType, Is.EqualTo(ResponseType.Html));
		}

		[Test]
		public void GetsCorrectResponseTypeWhenTypesArePrioritised()
		{
			var responseType = _acceptHeaderResponseTypeResolver.Resolve("text/xml; q=.8,text/html;q =.7,application/json;q= .9");
			Assert.That(responseType, Is.EqualTo(ResponseType.Json));
		}

		[Test]
		public void GetsCorrectResponseTypeWhenOneTypeIsPrioritised()
		{
			var responseType = _acceptHeaderResponseTypeResolver.Resolve("text/xml,text/html;q=.7,application/json");
			Assert.That(responseType, Is.EqualTo(ResponseType.Json));
		}
	}
}