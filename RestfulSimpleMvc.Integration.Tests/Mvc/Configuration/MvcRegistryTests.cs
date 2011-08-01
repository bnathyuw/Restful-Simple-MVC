using System.Web.Mvc;
using NUnit.Framework;
using RestfulSimpleMvc.Core;
using RestfulSimpleMvc.Core.Configuration;
using RestfulSimpleMvc.Core.Exceptions;
using RestfulSimpleMvc.Core.ResponseType;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.Results;
using RestfulSimpleMvc.Core.SerializationDataProviders;
using RestfulSimpleMvc.Core.StatusCodes;
using StructureMap;

namespace RestfulSimpleMvc.Integration.Tests.Mvc.Configuration
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
		public void CanResolveISerializationDataProviderForRestfulException() {
			var serializer = _container.GetInstance<SerializationDataProvider<RestfulException>>();
			Assert.That(serializer, Is.TypeOf(typeof(RestfulExceptionSerializationDataProvider)));
		}

		[Test]
		public void CanResolveDefaultStatusCodeWriter() {
			var statusCodeWriter = _container.GetInstance<IStatusCodeWriter>();
			Assert.That(statusCodeWriter, Is.TypeOf(typeof(DefaultStatusCodeWriter)));
		}

		[Test]
		public void CanResolveHtmlStatusCodeWriter() {
			var statusCodeWriter = _container.GetInstance<IStatusCodeWriter>(ResponseType.Html.ToString());
			Assert.That(statusCodeWriter, Is.TypeOf(typeof(HtmlStatusCodeWriter)));
		}

		[Test]
		public void CanResolveJsonStatusCodeWriter()
		{
			var statusCodeWriter = _container.GetInstance<IStatusCodeWriter>(ResponseType.Json.ToString());
			Assert.That(statusCodeWriter, Is.TypeOf(typeof(DefaultStatusCodeWriter)));
		}

        [Test]
        public void CanResolveSerializationDataProviderFromExternalProject()
        {
            var serializationDataProvider = _container.GetInstance<SerializationDataProvider<EntityStub>>();
            Assert.That(serializationDataProvider, Is.TypeOf((typeof(EntityStubSerializationDataProvider))));
        }
	}
}