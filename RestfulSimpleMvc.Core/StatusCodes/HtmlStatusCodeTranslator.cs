using System.Collections.Generic;
using System.Net;

namespace RestfulSimpleMvc.Core.StatusCodes {
	public class HtmlStatusCodeTranslator : IStatusCodeTranslator {
		private readonly Dictionary<ResourceStatus, HttpStatusCode> _lookUp = new Dictionary<ResourceStatus, HttpStatusCode> {
		                                                                                                            	{ResourceStatus.Created, HttpStatusCode.MovedPermanently},
		                                                                                                            	{ResourceStatus.Moved, HttpStatusCode.MovedPermanently},
		                                                                                                            	{ResourceStatus.Deleted, HttpStatusCode.MovedPermanently}
		                                                                                                            };

		public HttpStatusCode LookUp(ResourceStatus status) {
			return _lookUp[status];
		}
	}
}