using System;
using RestfulSimpleMvc.Core.ResponseWriters;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace RestfulSimpleMvc.Core.Configuration
{
	public class ResponseWriterConvention:IRegistrationConvention
	{
		public void Process(Type type, Registry registry) {
			if (!type.CanBeCastTo(typeof(IResponseWriter))) return;

			var name = type.Name.Replace("ResponseWriter", "");

			registry.AddType(typeof (IResponseWriter), type, name);
		}
	}
}