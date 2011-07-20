using System.Web.Mvc;
using NUnit.Framework;
using RestfulSimpleMvc.Core;
using RestfulSimpleMvc.Core.Configuration;
using RestfulSimpleMvc.Core.Exceptions;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.SerializationDataProviders;
using StructureMap;

namespace RestfulSimpleMvc.Integration.Tests.Mvc.Configuration
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
		public void CanResolveISerializationDataProviderForRestfulException() {
			var serializer = _container.GetInstance<SerializationDataProvider<RestfulException>>();
			Assert.That(serializer, Is.TypeOf(typeof(RestfulExceptionSerializationDataProvider)));
		}
	}
}