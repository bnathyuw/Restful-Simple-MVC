using System.Web.Mvc;
using RestfulSimpleMvc.Core.Location;
using RestfulSimpleMvc.Web.Models;

namespace RestfulSimpleMvc.Web.Location {
	public class PostLocationProvider:LocationProvider<Post> {
		protected override string GetLocation(Post content, ControllerContext context) {
			var urlHelper = new UrlHelper(context.RequestContext);
			return urlHelper.Action("Get", "Post", new {content.Id});
		}
	}
}