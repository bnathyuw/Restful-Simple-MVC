using System;

namespace Playground.Mvc
{
	public class RouteDataResponseTypeResolver : IResponseTypeResolver
	{
		public ResponseType? Resolve(string sourceString)
		{
			if (sourceString != null && !String.IsNullOrEmpty(sourceString))
			{
				ResponseType responseType;
				if (Enum.TryParse(sourceString, true, out responseType))
					return responseType;
			}
			return null;
		}
	}
}