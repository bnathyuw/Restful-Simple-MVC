using System;
using System.Net;

namespace Playground.Mvc.Exceptions
{
	public class InternalServerErrorException : RestfulException
	{
		public InternalServerErrorException(string message = null, Exception innerException = null)
			: base(HttpStatusCode.InternalServerError, message, innerException)
		{

		}
	}
}