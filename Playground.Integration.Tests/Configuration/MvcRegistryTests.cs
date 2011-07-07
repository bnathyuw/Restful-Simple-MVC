using System.Web.Mvc;
using NUnit.Framework;
using Playground.Web.Configuration;
using Playground.Web.Mvc;

namespace Playground.Integration.Tests.Configuration
{
	[TestFixture]
	public class MvcRegistryTests
	{
		private readonly StructureMapDependencyResolver _dependencyResolver;

		public MvcRegistryTests() {
			_dependencyResolver = new StructureMapDependencyResolver(new StructureMap.Container());
		}

		[Test]
		public void CanResolveActionInvoker() {
			var actionInvoker = _dependencyResolver.GetService(typeof (IActionInvoker));
			Assert.That(actionInvoker, Is.TypeOf(typeof(RestfulActionInvoker)));
		}

		[Test]
		public void CanResolveTypedResultFactory() {
			var typedResultFactory = _dependencyResolver.GetService(typeof (ITypedResultFactory));
			Assert.That(typedResultFactory, Is.TypeOf((typeof(TypedResultFactory))));
		}
	}
}