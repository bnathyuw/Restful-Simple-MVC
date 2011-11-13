using System.Net;

namespace RestfulSimpleMvc.Core.StatusCodes {
	public interface IStatusCodeTranslator {
		HttpStatusCode LookUp(ResourceStatus status);
	}
}