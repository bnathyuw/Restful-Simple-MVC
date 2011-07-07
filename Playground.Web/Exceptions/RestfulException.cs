using System;
using System.Net;

namespace Playground.Web.Exceptions
{
	public abstract class RestfulException : Exception
	{
		private readonly HttpStatusCode _httpStatusCode;

		protected RestfulException(HttpStatusCode httpStatusCode, string message = null, Exception innerException = null)
			: base(message, innerException)
		{
			_httpStatusCode = httpStatusCode;
		}

		public HttpStatusCode HttpStatusCode { get { return _httpStatusCode; } }
	}
}