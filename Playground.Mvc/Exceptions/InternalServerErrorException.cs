using System;
using System.Net;

namespace RestfulSimpleMvc.Core.Exceptions
{
	public class InternalServerErrorException : RestfulException
	{
		public InternalServerErrorException(string message = null, Exception innerException = null)
			: base(HttpStatusCode.InternalServerError, message, innerException)
		{

		}
	}
}