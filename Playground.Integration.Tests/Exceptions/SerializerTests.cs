using System.IO;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NUnit.Framework;
using Playground.Mvc.Exceptions;

namespace Playground.Integration.Tests.Exceptions
{
	[TestFixture]
	public class SerializerTests
	{
		[Test]
		public void ExceptionCanBeSerializedToXml() {
			var exception = new NotFoundException();
			var serializer = new XmlSerializer(exception.GetType());
			Stream stream = new MemoryStream();
			serializer.Serialize(stream, exception);
		}

		[Test]
		public void ExceptionCanBeSerializedToJson()
		{
			var exception = new NotFoundException();
			var serializer = new JavaScriptSerializer();
			var output = serializer.Serialize(exception);
			Assert.That(output, Is.Not.Null);
		}
	}
}