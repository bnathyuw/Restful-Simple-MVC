using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace Playground.Mvc.Serializers
{
	public class DefaultSerializer<T>: ISerializer<T> {

		public void WriteXmlToStream(object content, Stream stream) {
			WriteXmlToStream((T) content, stream);
		}

		public void WriteJsonToStream(object content, Stream stream) {
			WriteJsonToStream((T)content, stream);
		}

		public void WriteJsonToStream(T content, Stream stream) {
			var output = GetJsonData(content);
			var serializer = new JavaScriptSerializer();
			var outputString = serializer.Serialize(output);
			var bytes = Encoding.UTF8.GetBytes(outputString);
			stream.Write(bytes, 0, bytes.Length);
		}

		public void WriteXmlToStream(T content, Stream stream) {
			var output = GetXmlData(content);
			var xmlWriter = XmlWriter.Create(stream);
			output.WriteTo(xmlWriter);
			xmlWriter.Close();
		}

		protected virtual dynamic GetJsonData(T content) {
			return content;
		}

		protected virtual XDocument GetXmlData(T content) {
			return new XDocument("root");
		}
	}
}