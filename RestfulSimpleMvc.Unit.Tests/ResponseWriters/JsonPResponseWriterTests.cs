using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using RestfulSimpleMvc.Core.ResponseWriters;
using RestfulSimpleMvc.Core.Serialization;
using Rhino.Mocks;

namespace RestfulSimpleMvc.Unit.Tests.ResponseWriters
{
	[TestFixture]
	public class JsonPResponseWriterTests
	{
		private const string JSON_OUTPUT = "jsonString";
		private const string CALLBACK = "callback";
		private IJsonSerializer _jsonSerializer;
		private IResponseUpdater _responseUpdater;
		private ISerializationDataProvider _serializationDataProvider;
		private ISerializationDataProviderFactory _serializationDataProviderFactory;
		private JsonPResponseWriter _responseWriter;
		private ControllerContext _controllerContext;
		private RouteData _routeData;

		[SetUp]
		public void SetUp() {
			_jsonSerializer = MockRepository.GenerateStub<IJsonSerializer>();
			_responseUpdater = MockRepository.GenerateStub<IResponseUpdater>();
			_serializationDataProvider = MockRepository.GenerateStub<ISerializationDataProvider>();
			_serializationDataProviderFactory = MockRepository.GenerateStub<ISerializationDataProviderFactory>();
			_serializationDataProviderFactory.Stub(f => f.Build(Arg<object>.Is.Anything)).Return(_serializationDataProvider);
			_responseWriter = new JsonPResponseWriter(_jsonSerializer, _responseUpdater,_serializationDataProviderFactory);
			_controllerContext = MockRepository.GenerateStrictMock<ControllerContext>();
			_routeData = new RouteData();
			_controllerContext.Stub(c => c.RouteData).Return(_routeData);
			_routeData.Values.Add("callback", CALLBACK);
			_jsonSerializer.Stub(s => s.Serialize(Arg<object>.Is.Anything)).Return(JSON_OUTPUT);
		}

		[Test]
		public void WrapsOutputInCallback() {
			_routeData.Values.Add("action", "Get");
			_responseWriter.WriteResponse(_controllerContext, null, null);

			_responseUpdater.AssertWasCalled(w => w.WriteOutputToResponse(_controllerContext, string.Format("{0}({1})", CALLBACK, JSON_OUTPUT)));
		}
	}
}