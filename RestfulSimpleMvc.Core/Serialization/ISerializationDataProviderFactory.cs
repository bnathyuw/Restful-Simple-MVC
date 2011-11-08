namespace RestfulSimpleMvc.Core.Serialization
{
	public interface ISerializationDataProviderFactory
	{
		ISerializationDataProvider Build(object content);
	}
}