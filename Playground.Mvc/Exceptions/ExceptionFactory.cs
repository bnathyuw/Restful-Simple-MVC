using System;
using System.Net;

namespace RestfulSimpleMvc.Core.Exceptions
{
	public static class ExceptionFactory {
		public static RestfulException Build(HttpStatusCode httpStatusCode, string message = null, Exception innerException = null)
		{
			switch(httpStatusCode) {
				case HttpStatusCode.Accepted:
					throw new Exception(message, innerException);
				case HttpStatusCode.Ambiguous:
					return new AmbiguousException(message, innerException);
				case HttpStatusCode.BadGateway:
					return new BadGatewayException(message, innerException);
				case HttpStatusCode.InternalServerError:
					return new InternalServerErrorException(message, innerException);
				default:
					return new NotFoundException(message, innerException);
			}
		}
	}
}