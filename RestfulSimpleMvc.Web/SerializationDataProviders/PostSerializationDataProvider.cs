using System.Xml.Linq;
using RestfulSimpleMvc.Core.Serialization;
using RestfulSimpleMvc.Web.Models;

namespace RestfulSimpleMvc.Web.SerializationDataProviders {
	public class PostSerializationDataProvider:SerializationDataProvider<Post> {
		protected override dynamic GetJsonData(Post content) {
			return new {Name = "Post"};
		}

		protected override XDocument GetXmlData(Post content) {
			return new XDocument(new XElement("Post"));
		}
	}
}