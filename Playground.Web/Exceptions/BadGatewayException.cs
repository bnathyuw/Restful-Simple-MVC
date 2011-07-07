using System;
using System.Net;

namespace Playground.Web.Exceptions
{
	public class BadGatewayException : RestfulException
	{
		public BadGatewayException(string message = null, Exception innerException = null)
			: base(HttpStatusCode.BadGateway, message, innerException)
		{

		}
	}
}