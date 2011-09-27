using System.Collections.Specialized;
using System.Web;
using System.Web.Routing;
using NUnit.Framework;
using RestfulSimpleMvc.Core.Routes;
using Rhino.Mocks;

namespace RestfulSimpleMvc.Unit.Tests.Routes.RestfulRouteTests {
    [TestFixture]
    public class GetRouteDataResponseTypeTests {
        private HttpContextBase _httpContext;
        private HttpRequestBase _httpRequest;
        private RestfulRoute _route;
		private readonly NameValueCollection _form = new NameValueCollection();
        private IAcceptHeaderResponseTypeResolver _acceptHeaderResponseTypeResolver;
        private const string CONTROLLER = "Methods";
        private const string URL = "Methods";
		private readonly NameValueCollection _headers = new NameValueCollection();

    	[SetUp]
        public void SetUp() {
    		_httpRequest = MockRepository.GenerateStub<HttpRequestBase>();
    		_httpRequest.Stub(r => r.PathInfo).Return("");
    		_httpRequest.Stub(r => r.Form).Return(_form);
    		_httpRequest.Stub(r => r.HttpMethod).Return("Get");
    		_httpRequest.Stub(r => r.Headers).Return(_headers);

    		_httpContext = MockRepository.GenerateStub<HttpContextBase>();
    		_httpContext.Stub(c => c.Request).Return(_httpRequest);
    		
			_acceptHeaderResponseTypeResolver = MockRepository.GenerateStub<IAcceptHeaderResponseTypeResolver>();
    		_route = new RestfulRoute(URL, CONTROLLER, new ResponseTypeMapper(_acceptHeaderResponseTypeResolver), new ActionMapper());
    	}

        [Test]
        public void DefaultIsXml() {
            StubUrl();
            var routeData = GetRouteData();
            Assert.That(routeData.Values["responseType"], Is.EqualTo(ResponseType.Xml));
        }

        [Test]
        public void HtmlResponseTypeIsPickedUpFromPath() {
            StubUrl(URL + ".html");
         
            var routeData = GetRouteData();
            Assert.That(routeData.Values["responseType"], Is.EqualTo(ResponseType.Html));
        }

        [Test]
        public void JsonResponseTypeIsPickedUpFromPath() {
            StubUrl(URL + ".json");

            var routeData = GetRouteData();
            Assert.That(routeData.Values["responseType"], Is.EqualTo(ResponseType.Json));
        }

        [Test]
        public void XmlResponseTypeIsPickedUpFromPath() {
            StubUrl(URL + ".xml");

            var routeData = GetRouteData();
            Assert.That(routeData.Values["responseType"], Is.EqualTo(ResponseType.Xml));
        }

        [Test]
        public void JunkResponseTypeInPathIsIgnored() {
            StubUrl(URL + ".junk");

            var routeData = GetRouteData();
            Assert.That(routeData.Values["responseType"], Is.EqualTo(ResponseType.Xml));
        }

        [Test]
        public void DoesNotCallAcceptHeaderResponseTypeResolverIfResponseTypeIsInPath() {
            StubUrl(URL + ".html");
            const string acceptValue = "text/html";
            StubAcceptHeader(acceptValue);

            GetRouteData();
            _acceptHeaderResponseTypeResolver.AssertWasNotCalled(r => r.Resolve(Arg<string>.Is.Anything));
        }

        [Test]
        public void CallsAcceptHeaderResponseTypeResolverIfNoResponseTypeInPath() {
            StubUrl();
            const string acceptValue = "text/html";
            StubAcceptHeader(acceptValue);

            GetRouteData();
            _acceptHeaderResponseTypeResolver.AssertWasCalled(r => r.Resolve(acceptValue));
        }

        [Test]
        public void ReturnsResponseTypeFromResponseTypeResolver() {
            StubUrl();
            const string acceptValue = "text/html";
            StubAcceptHeader(acceptValue);
            _acceptHeaderResponseTypeResolver.Stub(r => r.Resolve(Arg<string>.Is.Anything)).Return(ResponseType.Json);

            var routeData = GetRouteData();
            Assert.That(routeData.Values["responseType"], Is.EqualTo(ResponseType.Json));
        }

    	private void StubAcceptHeader(string value) {
            _headers.Add("Accept", value);
        }

        private void StubUrl(string url = URL) {
            _httpRequest.Stub(r => r.AppRelativeCurrentExecutionFilePath).Return("~/" + url);
        }

        private RouteData GetRouteData() {
            var routeData = _route.GetRouteData(_httpContext);

            Assert.That(routeData != null, "No matching route found");
            return routeData;
        }
    }
}