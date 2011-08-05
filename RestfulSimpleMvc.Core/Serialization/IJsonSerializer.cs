namespace RestfulSimpleMvc.Core.Serialization
{
	public interface IJsonSerializer
	{
		string Serialize(object content);
	}
}