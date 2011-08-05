using System.Web.Script.Serialization;

namespace RestfulSimpleMvc.Core.Serialization
{
	public class JsonSerializer:IJsonSerializer
	{

		public string Serialize(object content) {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(content);
		}
	}
}