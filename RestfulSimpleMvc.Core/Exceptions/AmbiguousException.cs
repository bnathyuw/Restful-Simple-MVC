using System;
using System.Net;

namespace RestfulSimpleMvc.Core.Exceptions
{
	public class AmbiguousException : RestfulException
	{
		public AmbiguousException(string message = null, Exception innerException = null)
			: base(HttpStatusCode.Ambiguous, message, innerException)
		{

		}
	}
}