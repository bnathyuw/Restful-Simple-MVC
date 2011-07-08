using System.Linq;

namespace Playground.Mvc
{
	public class AcceptHeaderResponseTypeResolver : IResponseTypeResolver
	{
		private readonly IAcceptHeaderParser _acceptHeaderParser;
		private readonly IEnumNameParser<ResponseType> _enumNameParser;

		public AcceptHeaderResponseTypeResolver(IAcceptHeaderParser acceptHeaderParser, IEnumNameParser<ResponseType> enumNameParser) {
			_acceptHeaderParser = acceptHeaderParser;
			_enumNameParser = enumNameParser;
		}

		public ResponseType? Resolve(string sourceString)
		{
			if (string.IsNullOrWhiteSpace(sourceString)) return null;

			var acceptedTypes = _acceptHeaderParser.GetAcceptedTypes(sourceString);
			var types = _enumNameParser.ParseNames();

			foreach (var group in acceptedTypes)
			{
				foreach (var type in types)
					if (group.Contains(type.Key))
						return type.Value;

			}
			return ResponseType.Xml;
		}
	}
}