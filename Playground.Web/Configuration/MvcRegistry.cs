using System.Web.Mvc;
using Playground.Web.Mvc;
using StructureMap.Configuration.DSL;

namespace Playground.Web.Configuration
{
	internal class MvcRegistry:Registry
	{
		public MvcRegistry() {
			For<IActionInvoker>().Use<MyActionInvoker>();

			SetAllProperties(c => c.OfType<IActionInvoker>());
		}
	}
}