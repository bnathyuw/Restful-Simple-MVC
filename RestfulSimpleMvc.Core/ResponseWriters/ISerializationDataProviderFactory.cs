using RestfulSimpleMvc.Core.Serialization;

namespace RestfulSimpleMvc.Core.ResponseWriters
{
	public interface ISerializationDataProviderFactory
	{
		ISerializationDataProvider Build(object content);
	}
}