using System.Collections.Specialized;
using System.Web;
using System.Web.Routing;
using NUnit.Framework;
using RestfulSimpleMvc.Core.Routes;
using Rhino.Mocks;

namespace RestfulSimpleMvc.Unit.Tests.Routes.RestfulRouteTests {
    [TestFixture]
    public class GetRouteDataActionTests {
        private HttpContextBase _httpContext;
        private HttpRequestBase _httpRequest;
        private RestfulRoute _route;
		private readonly NameValueCollection _form = new NameValueCollection();
		private readonly NameValueCollection _headers = new NameValueCollection();
    	private readonly NameValueCollection _queryString = new NameValueCollection();
    	private const string CONTROLLER = "Methods";
        private const string URL = "Methods";

        [SetUp]
        public void SetUp() {
        	_httpRequest = MockRepository.GenerateStub<HttpRequestBase>();
        	_httpRequest.Stub(r => r.AppRelativeCurrentExecutionFilePath).Return("~/" + URL);
        	_httpRequest.Stub(r => r.PathInfo).Return("");
        	_httpRequest.Stub(r => r.Form).Return(_form);
        	_httpRequest.Stub(r => r.Headers).Return(_headers);
        	_httpRequest.Stub(r => r.QueryString).Return(_queryString);

        	_httpContext = MockRepository.GenerateStub<HttpContextBase>();
        	_httpContext.Stub(c => c.Request).Return(_httpRequest);

        	var acceptHeaderResponseTypeResolver = MockRepository.GenerateStub<IAcceptHeaderResponseTypeResolver>();
            _route = new RestfulRoute(URL, CONTROLLER, new ResponseTypeMapper(acceptHeaderResponseTypeResolver), new ActionMapper());
        }

        [Test]
        public void GetCorrespondsToGetMethod() {
            StubHttpMethod("Get");
			var routeData = GetRouteData();
            Assert.That(routeData.Values["action"], Is.EqualTo("Get"));
        }

    	[Test]
        public void PutCorrespondsToPutMethod() {
			StubHttpMethod("Put");
			var routeData = GetRouteData();
            Assert.That(routeData.Values["action"], Is.EqualTo("Put"));
        }

    	[Test]
        public void PostCorrespondsToPostMethod() {
			StubHttpMethod("Post");
            var routeData = GetRouteData();
    		Assert.That(routeData.Values["action"], Is.EqualTo("Post"));
        }

    	[Test]
        public void PostWithActionParamCorrespondsToPutMethod() {
			StubHttpMethod("Post");
            _form.Add("_action", "Put");
			var routeData = GetRouteData();
            Assert.That(routeData.Values["action"], Is.EqualTo("Put"));
        }

    	private void StubHttpMethod(string methodName) {
    		_httpRequest.Stub(r => r.HttpMethod).Return(methodName);
    	}

    	private RouteData GetRouteData() {
    		var routeData = _route.GetRouteData(_httpContext);

    		Assert.That(routeData != null);
    		return routeData;
    	}
    }
}