namespace RestfulSimpleMvc.Core.ResponseWriters
{
	public interface IResponseWriterFactory {
		IResponseWriter Build(ResponseType responseType);
	}
}