using System.Collections.Specialized;
using System.Web;
using System.Web.Routing;
using NUnit.Framework;
using RestfulSimpleMvc.Core.Routes;
using Rhino.Mocks;

namespace RestfulSimpleMvc.Unit.Tests.Routes.RestfulRouteTests {
    [TestFixture]
    public class GetVirtualPathResponseTypeTests {
    	private HttpRequestBase _httpRequest;
    	private HttpContextBase _httpContext;
    	private readonly NameValueCollection _form = new NameValueCollection();
    	private readonly NameValueCollection _headers = new NameValueCollection();
    	private IAcceptHeaderResponseTypeResolver _acceptHeaderResponseTypeResolver;
    	private RestfulRoute _route;
    	private RequestContext _requestContext;
    	private RouteData _routeData;
    	private const string URL = "Methods";
    	private const string CONTROLLER = "Methods";

    	[SetUp]
        public void SetUp() {
    		_httpRequest = MockRepository.GenerateStub<HttpRequestBase>();
    		_httpRequest.Stub(r => r.PathInfo).Return("");
    		_httpRequest.Stub(r => r.HttpMethod).Return("Get");
    		_httpRequest.Stub(r => r.Form).Return(_form);
    		_httpRequest.Stub(r => r.Headers).Return(_headers);

    		_httpContext = MockRepository.GenerateStub<HttpContextBase>();
    		_httpContext.Stub(c => c.Request).Return(_httpRequest);

			_routeData = MockRepository.GenerateStub<RouteData>();

			_requestContext = MockRepository.GenerateStub<RequestContext>();
    		_requestContext.RouteData = _routeData;
    		_requestContext.HttpContext = _httpContext;

    		_acceptHeaderResponseTypeResolver = MockRepository.GenerateStub<IAcceptHeaderResponseTypeResolver>();
    		_route = new RestfulRoute(URL, CONTROLLER, new ResponseTypeMapper(_acceptHeaderResponseTypeResolver), new ActionMapper());
    	}

    	[Test]
    	public void IfNoResponseTypeIsRequestedDoNotAddSuffix() {
    		var virtualPathData = GetVirtualPath(new { action = "Get", controller = CONTROLLER});
			Assert.AreEqual(URL, virtualPathData.VirtualPath);
    	}

    	[Test] public void IfResponseTypeRequestedIsXmlDoNotAddSuffix() {
			var virtualPathData = GetVirtualPath(new { action = "Get", controller = CONTROLLER, responseType = ResponseType.Xml });
			Assert.AreEqual(URL, virtualPathData.VirtualPath);
		}

		[Test]
		public void IfResponseTypeRequestIsNotXmlAddSuffix() {
			var virtualPathData = GetVirtualPath(new { action = "Get", controller = CONTROLLER, responseType = ResponseType.Json });
			Assert.AreEqual(URL + ".json", virtualPathData.VirtualPath);
		}

    	[Test]
    	public void IfAcceptHeaderResponseTypeIsSameAsExplicitResponseTypeDoNotAddSuffix() {
    		_acceptHeaderResponseTypeResolver.Stub(r => r.Resolve(Arg<string>.Is.Anything)).Return(ResponseType.Json);
			var virtualPathData = GetVirtualPath(new { action = "Get", controller = CONTROLLER, responseType = ResponseType.Json });
			Assert.AreEqual(URL, virtualPathData.VirtualPath);
    	}

		[Test]
		public void IfAcceptHeaderResponseTypeIsDifferentFromExplicitResponseTypeAddSuffix() {
			_acceptHeaderResponseTypeResolver.Stub(r => r.Resolve(Arg<string>.Is.Anything)).Return(ResponseType.Html);
			var virtualPathData = GetVirtualPath(new { action = "Get", controller = CONTROLLER, responseType = ResponseType.Json });
			Assert.AreEqual(URL + ".json", virtualPathData.VirtualPath);
		}

    	private VirtualPathData GetVirtualPath(object routeValues) {
			var routeValueDictionary = new RouteValueDictionary(routeValues);
			var virtualPathData = _route.GetVirtualPath(_requestContext, routeValueDictionary);
			Assert.That(virtualPathData != null);
			return virtualPathData;
		}
    }
}