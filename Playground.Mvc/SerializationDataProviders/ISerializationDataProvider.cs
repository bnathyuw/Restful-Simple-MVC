using System.Xml.Linq;

namespace RestfulSimpleMvc.Core.SerializationDataProviders
{
	public interface ISerializationDataProvider
	{
		dynamic GetJsonData(object content);
		XDocument GetXmlData(object content);
	}

	public interface ISerializationDataProvider<in T> : ISerializationDataProvider{}
}