using System.Web.Mvc;
using RestfulSimpleMvc.Web.Models;

namespace RestfulSimpleMvc.Web.Controllers
{
	public class MethodsController:Controller
	{
		public Method Put(Method method) {
			return method;
		}
	}
}