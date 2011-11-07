using System;
using RestfulSimpleMvc.Core.StatusCodes;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace RestfulSimpleMvc.Core.Configuration {
	public class StatusCodeProviderConvention:IRegistrationConvention {
		public void Process(Type type, Registry registry) {
			if (!type.CanBeCastTo(typeof(IStatusCodeTranslator))) return;

			var name = type.Name.Replace("StatusCodeProvider", "");

			registry.AddType(typeof (IStatusCodeTranslator), type, name);
		}
	}
}