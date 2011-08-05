using System.Xml.Linq;

namespace RestfulSimpleMvc.Core.Serialization
{
	public abstract class SerializationDataProvider<T>: ISerializationDataProvider {
		public dynamic GetJsonData(object content) {
			return GetJsonData((T) content);
		}

		public XDocument GetXmlData(object content) {
			return GetXmlData((T) content);
		}

		protected abstract dynamic GetJsonData(T content);

		protected abstract XDocument GetXmlData(T content);
	}
}