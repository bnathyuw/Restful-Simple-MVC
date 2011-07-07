using System;
using System.Net;

namespace Playground.Web.Exceptions
{
	public class InternalServerErrorException : RestfulException
	{
		public InternalServerErrorException(string message = null, Exception innerException = null)
			: base(HttpStatusCode.InternalServerError, message, innerException)
		{

		}
	}
}