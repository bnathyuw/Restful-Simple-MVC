using System;
using System.Net;

namespace Playground.Mvc.Exceptions
{
	public class BadGatewayException : RestfulException
	{
		public BadGatewayException(string message = null, Exception innerException = null)
			: base(HttpStatusCode.BadGateway, message, innerException)
		{

		}
	}
}