using System;
using Playground.Mvc.ResponseWriters;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Playground.Mvc.Configuration
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