using System.Xml.Linq;

namespace RestfulSimpleMvc.Core.SerializationDataProviders
{
	public class DefaultSerializationDataProvider<T>: ISerializationDataProvider<T> {
		public dynamic GetJsonData(object content) {
			return GetJsonData((T) content);
		}

		public XDocument GetXmlData(object content) {
			return GetXmlData((T) content);
		}

		protected virtual dynamic GetJsonData(T content) {
			return content;
		}

		protected virtual XDocument GetXmlData(T content) {
			return new XDocument("root");
		}
	}
}