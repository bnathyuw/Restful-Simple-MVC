using System.Web.Mvc;
using NUnit.Framework;
using Playground.Mvc;
using Playground.Mvc.Configuration;
using Playground.Mvc.Exceptions;
using Playground.Mvc.ResponseWriters;
using Playground.Mvc.SerializationDataProviders;
using StructureMap;

namespace Playground.Integration.Tests.Mvc.Configuration
{
	[TestFixture]
	public class MvcRegistryTests
	{
		private readonly IContainer _container;

		public MvcRegistryTests() {
			_container = new Container();
			_container.Configure(x => x.AddRegistry(new MvcRegistry()));
		}

		[Test]
		public void ConfigurationIsValid() {
			_container.AssertConfigurationIsValid();
		}

		[Test]
		public void CanResolveActionInvoker() {
			var actionInvoker = _container.GetInstance<IActionInvoker>();
			Assert.That(actionInvoker, Is.TypeOf(typeof(RestfulActionInvoker)));
		}

		[Test]
		public void CanResolveTypedResultFactory() {
			var typedResultFactory = _container.GetInstance<ITypedResultFactory>();
			Assert.That(typedResultFactory, Is.TypeOf((typeof(TypedResultFactory))));
		}

		[Test]
		public void CanResolveResponseWriterForHtml() {
			var responseWriter = _container.GetInstance<IResponseWriter>(ResponseType.Html.ToString());
			Assert.That(responseWriter, Is.TypeOf((typeof(HtmlResponseWriter))));
		}

		[Test]
		public void CanResolveResponseWriterForJson()
		{
			var responseWriter = _container.GetInstance<IResponseWriter>(ResponseType.Json.ToString());
			Assert.That(responseWriter, Is.TypeOf((typeof(JsonResponseWriter))));
		}

		[Test]
		public void CanResolveResponseWriterForXml()
		{
			var responseWriter = _container.GetInstance<IResponseWriter>(ResponseType.Xml.ToString());
			Assert.That(responseWriter, Is.TypeOf((typeof(XmlResponseWriter))));
		}

		[Test]
		public void CanResolveISerializationDataProviderForAmbiguousException()
		{
			var serializer = _container.GetInstance<ISerializationDataProvider<AmbiguousException>>();
			Assert.That(serializer, Is.TypeOf(typeof(RestfulExceptionSerializationDataProvider)));
		}

		[Test]
		public void CanResolveISerializationDataProviderForBadGatewayException()
		{
			var serializer = _container.GetInstance<ISerializationDataProvider<BadGatewayException>>();
			Assert.That(serializer, Is.TypeOf(typeof(RestfulExceptionSerializationDataProvider)));
		}

		[Test]
		public void CanResolveISerializationDataProviderForInternalServerErrorException()
		{
			var serializer = _container.GetInstance<ISerializationDataProvider<InternalServerErrorException>>();
			Assert.That(serializer, Is.TypeOf(typeof(RestfulExceptionSerializationDataProvider)));
		}

		[Test]
		public void CanResolveISerializationDataProviderForNotFoundException()
		{
			var serializer = _container.GetInstance<ISerializationDataProvider<NotFoundException>>();
			Assert.That(serializer, Is.TypeOf(typeof(NotFoundExceptionSerializationDataProvider)));
		}

		[Test]
		public void CanResolveISerializationDataProviderForRestfulException() {
			var serializer = _container.GetInstance<ISerializationDataProvider<RestfulException>>();
			Assert.That(serializer, Is.TypeOf(typeof(RestfulExceptionSerializationDataProvider)));
		}
	}
}