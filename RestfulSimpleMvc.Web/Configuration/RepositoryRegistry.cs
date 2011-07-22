using RestfulSimpleMvc.Web.Models;
using StructureMap.Configuration.DSL;

namespace RestfulSimpleMvc.Web.Configuration
{
	public class RepositoryRegistry:Registry
	{
		public RepositoryRegistry() {
			For<IAddressRepository>().Use<InMemoryAddressRepository>();
		}
	}
}