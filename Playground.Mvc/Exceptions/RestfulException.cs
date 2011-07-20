using System;
using System.Net;

namespace RestfulSimpleMvc.Core.Exceptions
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

		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("httpStatusCode", HttpStatusCode);
		}
	}
}