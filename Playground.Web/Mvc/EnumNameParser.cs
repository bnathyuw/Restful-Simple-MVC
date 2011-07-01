using System;
using System.Collections.Generic;
using System.Linq;

namespace Playground.Web.Mvc
{
	public class EnumNameParser<T> : IEnumNameParser<T>
	{
		public Dictionary<string, T> ParseNames()
		{
			var types = new Dictionary<string, T>();
			foreach (var type in Enum.GetNames(typeof(T)))
			{
				foreach (var stringRepresentation in GetNames(type))
					types.Add(stringRepresentation, (T)Enum.Parse(typeof(T), type));
			}
			return types;
		}
		private static IEnumerable<string> GetNames(string type)
		{
			var fieldInfo = typeof(T).GetField(type);
			var stringRepresentationsAttribute = (NamesAttribute)fieldInfo.GetCustomAttributes(typeof(NamesAttribute), false).FirstOrDefault();
			return stringRepresentationsAttribute != null ? stringRepresentationsAttribute.StringRepresentations : new List<string>();
		}

	}
}