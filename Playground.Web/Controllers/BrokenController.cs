using System;
using System.Web.Mvc;

namespace Playground.Web.Controllers
{
	public class BrokenController : Controller
	{
		public object Get()
		{
			throw new Exception("Oops! Something's gone wrong. Silly me: what a klutz!");
		}
	}
}