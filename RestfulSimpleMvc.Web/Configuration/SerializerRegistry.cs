using RestfulSimpleMvc.Core.SerializationDataProviders;
using StructureMap.Configuration.DSL;

namespace RestfulSimpleMvc.Web.Configuration
{
	public class SerializerRegistry:Registry
	{
		public SerializerRegistry() {
			Scan(x =>{
			     	x.TheCallingAssembly();
			     	x.ConnectImplementationsToTypesClosing(typeof (SerializationDataProvider<>));
			     });

		}
	}
}