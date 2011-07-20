using System;
using System.Net;

namespace RestfulSimpleMvc.Core.Exceptions
{
	public class BadGatewayException : RestfulException
	{
		public BadGatewayException(string message = null, Exception innerException = null)
			: base(HttpStatusCode.BadGateway, message, innerException)
		{

		}
	}
}