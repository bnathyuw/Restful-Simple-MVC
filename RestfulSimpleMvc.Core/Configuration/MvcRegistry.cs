﻿using System.Reflection;
using System.Web.Mvc;
using RestfulSimpleMvc.Core.SerializationDataProviders;
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
                     x.Convention<StatusCodeWriterConvention>();
                     x.ConnectImplementationsToTypesClosing(typeof (SerializationDataProvider<>));
                 });

            For<IStatusCodeWriter>().MissingNamedInstanceIs.TheInstanceNamed("Default");
            For<IActionInvoker>().Use<RestfulActionInvoker>();
            SetAllProperties(c => c.OfType<IActionInvoker>());
            For<IEnumNameParser<ResponseType.ResponseType>>().Use<EnumNameParser<ResponseType.ResponseType>>();
        }

        private static bool ExcludeAssemblies(Assembly assembly)
        {
            return !(assembly.FullName.StartsWith("System") || assembly.FullName.StartsWith("StructureMap"));
        }
    }
}