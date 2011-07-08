using System;
using System.Net;

namespace Playground.Mvc.Exceptions
{
	public class NotFoundException : RestfulException
	{
		public NotFoundException(string message = null, Exception innerException = null)
			: base(HttpStatusCode.NotFound, message, innerException)
		{

		}
	}
}