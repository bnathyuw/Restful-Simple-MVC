using System.Linq;

namespace RestfulSimpleMvc.Core
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

			foreach (var typeGroup in acceptedTypes) {
				var @group = typeGroup;
				foreach (var type in types.Where(type => @group.Contains(type.Key))) {
					return type.Value;
				}
			}
			return ResponseType.Xml;
		}
	}
}