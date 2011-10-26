using RestfulSimpleMvc.Core;

namespace RestfulSimpleMvc.Web.Models
{
	public class Post:ILocated	
	{
		public string GetLocation() {
			return "http://localhost/restful-simple-mvc/posts/abc";
		}
	}
}