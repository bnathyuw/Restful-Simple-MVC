using System.Collections.Generic;
using System.Net;

namespace RestfulSimpleMvc.Core.StatusCodes {
	public class DefaultStatusCodeTranslator : IStatusCodeTranslator {
		private readonly Dictionary<ResourceStatus, HttpStatusCode> _lookUp = new Dictionary<ResourceStatus, HttpStatusCode> {
		                                                                                                            	{ResourceStatus.Created, HttpStatusCode.Created},
		                                                                                                            	{ResourceStatus.Moved, HttpStatusCode.MovedPermanently},
		                                                                                                            	{ResourceStatus.Deleted, HttpStatusCode.NoContent}
		                                                                                                            };

		public HttpStatusCode LookUp(ResourceStatus status) {
			return _lookUp[status];
		}
	}
}