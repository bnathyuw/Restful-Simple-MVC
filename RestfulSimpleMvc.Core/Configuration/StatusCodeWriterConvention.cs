using System;
using RestfulSimpleMvc.Core.StatusCodes;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace RestfulSimpleMvc.Core.Configuration
{
	public class StatusCodeWriterConvention:IRegistrationConvention
	{
		public void Process(Type type, Registry registry) {
			if (!type.CanBeCastTo(typeof(IStatusCodeWriter))) return;

			var name = type.Name.Replace("StatusCodeWriter", "");

			registry.AddType(typeof (IStatusCodeWriter), type, name);
		}
	}
}