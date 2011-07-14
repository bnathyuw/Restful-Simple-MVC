using System.Web.Mvc;
using Playground.Web.Models;

namespace Playground.Web.Controllers
{
	public class HomeController : Controller
    {
		public object Get() {
        	return new Home("10 Downing Street", "London");
        }

    }
}
