using System;
using System.Net;

namespace Playground.Web.Exceptions
{
	public class AmbiguousException : RestfulException
	{
		public AmbiguousException(string message = null, Exception innerException = null)
			: base(HttpStatusCode.Ambiguous, message, innerException)
		{

		}
	}
}