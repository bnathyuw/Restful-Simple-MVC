using System.Web.Mvc;
using RestfulSimpleMvc.Web.Models;

namespace RestfulSimpleMvc.Web.Controllers
{
	public class HomeController : Controller
    {
		public object Get() {
        	return new Home("10 Downing Street", "London", new Inhabitant("Larry"), new Inhabitant("Sibyl"), new Inhabitant("Humphrey"));
        }

    }
}
