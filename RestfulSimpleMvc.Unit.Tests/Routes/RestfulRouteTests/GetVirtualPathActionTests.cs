using System.Collections.Specialized;
using System.Web;
using System.Web.Routing;
using NUnit.Framework;
using RestfulSimpleMvc.Core.Routes;
using Rhino.Mocks;

namespace RestfulSimpleMvc.Unit.Tests.Routes.RestfulRouteTests {
    [TestFixture]
    public class GetVirtualPathActionTests {
        private HttpContextBase _httpContext;
        private HttpRequestBase _httpRequest;
        private RestfulRoute _route;
		private readonly NameValueCollection _form = new NameValueCollection();
		private readonly NameValueCollection _headers = new NameValueCollection();
    	private RequestContext _requestContext;
    	private readonly RouteData _routeData = new RouteData();
    	private const string CONTROLLER = "Methods";
        private const string URL = "Methods";

        [SetUp]
        public void SetUp() {
			_httpRequest = MockRepository.GenerateStub<HttpRequestBase>();
        	_httpRequest.Stub(r => r.AppRelativeCurrentExecutionFilePath).Return("~/" + URL);
        	_httpRequest.Stub(r => r.PathInfo).Return("");
        	_httpRequest.Stub(r => r.Form).Return(_form);
        	_httpRequest.Stub(r => r.Headers).Return(_headers);

        	_httpContext = MockRepository.GenerateStub<HttpContextBase>();
        	_httpContext.Stub(c => c.Request).Return(_httpRequest);
             
			_requestContext = MockRepository.GenerateStub<RequestContext>();
         	_requestContext.RouteData = _routeData;
            _requestContext.HttpContext = _httpContext;
       	
			var acceptHeaderResponseTypeResolver = MockRepository.GenerateStub<IAcceptHeaderResponseTypeResolver>();
        	_route = new RestfulRoute(URL, CONTROLLER, new ResponseTypeMapper(acceptHeaderResponseTypeResolver), new ActionMapper());
       }

        [Test]
        public void GetReturnsCorrectPath() {
            var virtualPathData = GetVirtualPathData(new { controller = CONTROLLER, action = "Get" });
            Assert.That(virtualPathData.VirtualPath, Is.EqualTo(URL));
        }

        [Test]
        public void PostReturnsCorrectPath() {
			var virtualPathData = GetVirtualPathData(new { controller = CONTROLLER, action = "Post" });
            Assert.That(virtualPathData.VirtualPath, Is.EqualTo(URL));
        }

        [Test]
        public void PutReturnsCorrectPath() {
			var virtualPathData = GetVirtualPathData(new { controller = CONTROLLER, action = "Put" });
            Assert.That(virtualPathData.VirtualPath, Is.EqualTo(URL));
        }

        [Test]
        public void OptionsReturnsCorrectPath() {
			var virtualPathData = GetVirtualPathData(new { controller = CONTROLLER, action = "Options" });
        	Assert.That(virtualPathData.VirtualPath, Is.EqualTo(URL));
        }

    	private VirtualPathData GetVirtualPathData(object routeValues) {
    		var virtualPathData = _route.GetVirtualPath(_requestContext, new RouteValueDictionary(routeValues));
    		Assert.That(virtualPathData != null);
    		return virtualPathData;
    	}
    }
}