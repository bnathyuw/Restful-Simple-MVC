using System.Web.Mvc;
using RestfulSimpleMvc.Web.Models;

namespace RestfulSimpleMvc.Web.Controllers
{
    public class PostsController : Controller
    {
        public Post Post(Post model) {
        	return model;
        }
    }
}
