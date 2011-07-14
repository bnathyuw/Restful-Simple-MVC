using System.Web.Mvc;
using NUnit.Framework;
using Playground.Mvc;
using Playground.Mvc.Exceptions;
using Playground.Mvc.ResponseWriters;
using Playground.Mvc.SerializationDataProviders;
using Playground.Web.Configuration;
using StructureMap;

namespace Playground.Integration.Tests.Mvc.Configuration
{
	[TestFixture]
	public class MvcRegistryTests
	{
		private readonly IContainer _container;

		public MvcRegistryTests() {
			_container = StructureMapBootstrapper.Container;
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
		public void CanResolveIJsonSerializerForNotFoundException() {
			var serializer = _container.ForGenericType(typeof (ISerializationDataProvider<>))
				.WithParameters(typeof (NotFoundException))
				.GetInstanceAs<ISerializationDataProvider>();
			Assert.That(serializer, Is.TypeOf(typeof(RestfulExceptionSerializationDataProvider)));
		}
	}
}