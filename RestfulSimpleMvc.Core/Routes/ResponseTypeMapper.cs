using System;
using System.Web;
using System.Web.Routing;

namespace RestfulSimpleMvc.Core.Routes
{
	public class ResponseTypeMapper : IResponseTypeMapper
	{
		private readonly IAcceptHeaderResponseTypeResolver _acceptHeaderResponseTypeResolver;
		public ResponseTypeMapper(IAcceptHeaderResponseTypeResolver acceptHeaderResponseTypeResolver) {
			_acceptHeaderResponseTypeResolver = acceptHeaderResponseTypeResolver;
		}

		public void MapResponseType(HttpContextBase httpContext, RouteData routeData) {
			routeData.Values.Add("responseType", ParseResponseType(routeData) ?? _acceptHeaderResponseTypeResolver.Resolve(httpContext.Request.Headers["Accept"]) ?? ResponseType.Xml);
		}
		
		private static ResponseType? ParseResponseType(RouteData routeData) {
			var rt = routeData.Values["rt"] as string;
			if (rt == null) 
				return null;

			ResponseType responseType;
			return Enum.TryParse(rt, true, out responseType) ? responseType : (ResponseType?) null;
		}

        
	}
}