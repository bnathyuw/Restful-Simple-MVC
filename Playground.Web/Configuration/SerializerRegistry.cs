using System;
using System.IO;
using Playground.Mvc.Serializers;
using Playground.Web.Models;
using Playground.Web.Serializers;
using StructureMap.Configuration.DSL;

namespace Playground.Web.Configuration
{
	public class SerializerRegistry:Registry
	{
		public SerializerRegistry() {
			For(typeof (ISerializer<>)).Use(typeof(DefaultSerializer<object>));
			For<ISerializer<Home>>().Use<HomeSerializer>();
		}
	}
}