namespace Playground.Mvc.ResponseWriters
{
	public interface IResponseWriterFactory {
		IResponseWriter Build(ResponseType responseType);
	}
}