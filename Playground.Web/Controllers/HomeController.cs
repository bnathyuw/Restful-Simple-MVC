using System.Web.Mvc;
using Playground.Web.Models;

namespace Playground.Web.Controllers
{
	public class HomeController : Controller
    {
		public object Get() {
			//if (DateTime.Now.Millisecond % 2 == 0)
			//    throw new HomeException();
        	return new Home("25 Boscombe Road", "London");
        }

    }
}
