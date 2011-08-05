using System.Web.Mvc;
using RestfulSimpleMvc.Core.Serialization;

namespace RestfulSimpleMvc.Core.ResponseWriters
{
	public class JsonPResponseWriter : IResponseWriter
	{
		private readonly IJsonSerializer _jsonSerializer;
		private readonly IResponseUpdater _responseUpdater;
		private readonly ISerializationDataProviderFactory _serializationDataProviderFactory;

		public JsonPResponseWriter(IJsonSerializer jsonSerializer, IResponseUpdater responseUpdater, ISerializationDataProviderFactory serializationDataProviderFactory) {
			_jsonSerializer = jsonSerializer;
			_responseUpdater = responseUpdater;
			_serializationDataProviderFactory = serializationDataProviderFactory;
		}

		public void WriteResponse(ControllerContext controllerContext, object content, string viewName)
		{
			var serializer = _serializationDataProviderFactory.Build(content);
			var jsonData = serializer.GetJsonData(content);
			string output = _jsonSerializer.Serialize(jsonData);

			var callback = controllerContext.RouteData.Values["callback"];
			var wrappedOutput = string.Format("{0}({1})", callback, output);

			_responseUpdater.WriteOutputToResponse(controllerContext, wrappedOutput);
			_responseUpdater.SetContentType(controllerContext,"application/json-p");
		}
	}
}