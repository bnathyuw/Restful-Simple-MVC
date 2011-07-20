using StructureMap;

namespace RestfulSimpleMvc.Core.ResponseWriters
{
	public class ResponseWriterFactory : IResponseWriterFactory
	{
		private readonly IContainer _container;

		public ResponseWriterFactory(IContainer container) {
			_container = container;
		}

		public IResponseWriter Build(ResponseType responseType) {
			return _container.GetInstance<IResponseWriter>(responseType.ToString());
		}
	}
}