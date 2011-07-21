using System.Net;

namespace RestfulSimpleMvc.Core.StatusCodes
{
	public interface IStatusCoded
	{
		HttpStatusCode HttpStatusCode { get; }
	}
}