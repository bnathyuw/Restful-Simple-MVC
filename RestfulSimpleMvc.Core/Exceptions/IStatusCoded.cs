using System.Net;

namespace RestfulSimpleMvc.Core.Exceptions {
	public interface IStatusCoded {
		HttpStatusCode HttpStatusCode { get; }
	}
}