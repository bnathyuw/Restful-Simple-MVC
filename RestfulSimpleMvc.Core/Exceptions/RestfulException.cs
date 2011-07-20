using System;
using System.Net;

namespace RestfulSimpleMvc.Core.Exceptions
{
	public class RestfulException : Exception
	{
		private readonly HttpStatusCode _httpStatusCode;

		public RestfulException(HttpStatusCode httpStatusCode, string message = null, Exception innerException = null)
			: base(message, innerException)
		{
			_httpStatusCode = httpStatusCode;
		}

		public HttpStatusCode HttpStatusCode { get { return _httpStatusCode; } }

		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("httpStatusCode", HttpStatusCode);
		}

		public static RestfulException Ambiguous(string message = null, Exception innerException = null) {
			return new RestfulException(HttpStatusCode.Ambiguous, message, innerException);
		}

		public static RestfulException BadGateway(string message = null, Exception innerException = null)
		{
			return new RestfulException(HttpStatusCode.BadGateway, message, innerException);
		}

		public static RestfulException InternalServerError(string message = null, Exception innerException = null)
		{
			return new RestfulException(HttpStatusCode.InternalServerError, message, innerException);
		}

		public static RestfulException NotFound(string message = null, Exception innerException = null)
		{
			return new RestfulException(HttpStatusCode.NotFound, message, innerException);
		}
	}
}