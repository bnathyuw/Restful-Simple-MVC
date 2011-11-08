using System.Web.Mvc;
using RestfulSimpleMvc.Web.Models;

namespace RestfulSimpleMvc.Web.Controllers {
	public class PostController:Controller {
		public Post Get(Post model) {
			return model;
		}

		public Post Put(Post model) {
			return model;
		}

		public Post Delete(Post model) {
			return null;
		}
	}
}