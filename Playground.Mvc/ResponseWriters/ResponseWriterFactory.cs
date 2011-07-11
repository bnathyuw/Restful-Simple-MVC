namespace Playground.Mvc.ResponseWriters
{
	public class ResponseWriterFactory : IResponseWriterFactory
	{
		public IResponseWriter Build(ResponseType responseType) {
			switch (responseType)
			{
				case ResponseType.Html:
					return new HtmlResponseWriter();
				case ResponseType.Xml:
					return new XmlResponseWriter();
				case ResponseType.Json:
					return new JsonResponseWriter();
				default:
					return null;
			}
		}
	}
}