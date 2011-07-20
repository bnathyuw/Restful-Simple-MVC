using System.Net;
using NUnit.Framework;
using RestfulSimpleMvc.Core.Exceptions;

namespace RestfulSimpleMvc.Unit.Tests.Exceptions
{
	[TestFixture]
	public class RestfulExceptionTests
	{
		[Test]
		public void AmbiguousReturnsExceptionWithCorrectStatusCode() {
			var exception = RestfulException.Ambiguous();
			Assert.That(exception.HttpStatusCode, Is.EqualTo(HttpStatusCode.Ambiguous));
		}

		[Test]
		public void BadGatewayReturnsExceptionWithCorrectStatusCode()
		{
			var exception = RestfulException.BadGateway();
			Assert.That(exception.HttpStatusCode, Is.EqualTo(HttpStatusCode.BadGateway));
		}

		[Test]
		public void NotFoundReturnsExceptionWithCorrectStatusCode()
		{
			var exception = RestfulException.NotFound();
			Assert.That(exception.HttpStatusCode, Is.EqualTo(HttpStatusCode.NotFound));
		}
	}
}