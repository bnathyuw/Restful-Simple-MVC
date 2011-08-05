using System.Xml.Linq;

namespace RestfulSimpleMvc.Core.Serialization
{
	public interface ISerializationDataProvider
	{
		dynamic GetJsonData(object content);
		XDocument GetXmlData(object content);
	}
}