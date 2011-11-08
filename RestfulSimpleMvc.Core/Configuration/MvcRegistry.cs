using System.Reflection;
using System.Web.Mvc;
using RestfulSimpleMvc.Core.Location;
using RestfulSimpleMvc.Core.Routes;
using RestfulSimpleMvc.Core.Serialization;
using RestfulSimpleMvc.Core.StatusCodes;
using StructureMap.Configuration.DSL;

namespace RestfulSimpleMvc.Core.Configuration
{
    public class MvcRegistry:Registry
    {
        public MvcRegistry()
        {
            Scan(x =>
                 {
                     x.TheCallingAssembly();
                     x.AssembliesFromApplicationBaseDirectory(ExcludeAssemblies);
                     x.WithDefaultConventions();
                     x.Convention<ResponseWriterConvention>();
					 x.Convention<StatusCodeProviderConvention>();
                     x.ConnectImplementationsToTypesClosing(typeof (SerializationDataProvider<>));
                 	x.ConnectImplementationsToTypesClosing(typeof (LocationProvider<>));
                 });

            For<IActionInvoker>().Use<RestfulActionInvoker>();
            SetAllProperties(c => c.OfType<IActionInvoker>());
        	For<IStatusCodeTranslator>().MissingNamedInstanceIs.TheInstanceNamed("Default");
            For<IEnumNameParser<ResponseType>>().Use<EnumNameParser<ResponseType>>();
        }

        private static bool ExcludeAssemblies(Assembly assembly)
        {
            return !(assembly.FullName.StartsWith("System") || assembly.FullName.StartsWith("StructureMap"));
        }
    }
}