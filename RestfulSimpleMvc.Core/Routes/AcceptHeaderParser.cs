using System;
using System.Collections.Generic;
using System.Linq;

namespace RestfulSimpleMvc.Core.Routes
{
	public class AcceptHeaderParser : IAcceptHeaderParser
	{
		public IEnumerable<IGrouping<decimal, string>> GetAcceptedTypes(string accept)
		{
			return accept.Split(',').Select(ParseAcceptedType).OrderByDescending(x => x.Priority).GroupBy(x => x.Priority, x => x.Type);
		}

		private static AcceptedType ParseAcceptedType(string typeString)
		{
			var typeSpec = typeString.Split(';');
			var typeParams = typeSpec.Skip(1).Select(ParseTypeParam);
			var priority = typeParams.Any(p => p.Key == "q") ? typeParams.First(p => p.Key == "q").Value : 1;
			return new AcceptedType
			{
				Type = typeSpec.First().Trim(),
				Priority = priority
			};
		}

		private static KeyValuePair<string, decimal> ParseTypeParam(string paramString)
		{
			var paramSpec = paramString.Split('=');
			return new KeyValuePair<string, decimal>(paramSpec.First().Trim(), Decimal.Parse(paramSpec.ElementAt(1).Trim()));
		}
	}
}