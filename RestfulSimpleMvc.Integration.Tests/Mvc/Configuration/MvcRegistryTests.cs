using System.Web.Mvc;
using NUnit.Framework;
using RestfulSimpleMvc.Core;
using RestfulSimpleMvc.Core.Configuration;
using RestfulSimpleMvc.Core.Exceptions;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.Results;
using RestfulSimpleMvc.Core.Routes;
using RestfulSimpleMvc.Core.Serialization;
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
		public void CanResolveResponseWriterForJsonP() {
			var responseWriter = _container.GetInstance<IResponseWriter>(ResponseType.JsonP.ToString());
			Assert.That(responseWriter, Is.TypeOf((typeof(JsonPResponseWriter))));
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
        public void CanResolveSerializationDataProviderFromExternalProject()
        {
            var serializationDataProvider = _container.GetInstance<SerializationDataProvider<EntityStub>>();
            Assert.That(serializationDataProvider, Is.TypeOf((typeof(EntityStubSerializationDataProvider))));
        }

		[Test]
		public void Can_resolve_status_code_translator_for_html() {
			var statusCodeTranslator = _container.GetInstance<IStatusCodeTranslator>(ResponseType.Html.ToString());
			Assert.That(statusCodeTranslator, Is.TypeOf((typeof(HtmlStatusCodeTranslator))));
		}

		[Test]
		public void Can_resolve_status_code_translator_for_xml() {
			var statusCodeTranslator = _container.GetInstance<IStatusCodeTranslator>(ResponseType.Xml.ToString());
			Assert.That(statusCodeTranslator, Is.TypeOf((typeof(DefaultStatusCodeTranslator))));
		}

		[Test]
		public void Can_resolve_status_code_translator_for_json() {
			var statusCodeTranslator = _container.GetInstance<IStatusCodeTranslator>(ResponseType.Json.ToString());
			Assert.That(statusCodeTranslator, Is.TypeOf((typeof(DefaultStatusCodeTranslator))));
		}

		[Test]
		public void Can_resolve_status_code_translator_for_jsonp() {
			var statusCodeTranslator = _container.GetInstance<IStatusCodeTranslator>(ResponseType.JsonP.ToString());
			Assert.That(statusCodeTranslator, Is.TypeOf((typeof(DefaultStatusCodeTranslator))));
		}
	}
}