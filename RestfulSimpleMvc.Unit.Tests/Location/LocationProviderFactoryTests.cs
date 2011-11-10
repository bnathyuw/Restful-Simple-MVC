using NUnit.Framework;
using RestfulSimpleMvc.Core.Location;

namespace RestfulSimpleMvc.Unit.Tests.Location
{
	[TestFixture]
	public class LocationProviderFactoryTests
	{
		[Test]
		public void Returns_null_when_content_is_null() {
			var locationProviderFactory = new LocationProviderFactory(null);
			var locationProvider = locationProviderFactory.Build(null);
			Assert.That(locationProvider, Is.Null);
		} 
	}
}