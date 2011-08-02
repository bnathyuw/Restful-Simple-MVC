using System.Collections.Specialized;
using System.Web;
using System.Web.Routing;
using NUnit.Framework;
using RestfulSimpleMvc.Core;
using RestfulSimpleMvc.Core.ResponseType;
using Rhino.Mocks;

namespace RestfulSimpleMvc.Unit.Tests {
    [TestFixture]
    public class RestfulRouteResponseTypeTests {
        private HttpContextBase _httpContext;
        private HttpRequestBase _httpRequest;
        private RestfulRoute _route;
        private NameValueCollection _form;
        private IAcceptHeaderResponseTypeResolver _acceptHeaderResponseTypeResolver;
        private const string CONTROLLER = "Methods";
        private const string URL = "Methods";
        private NameValueCollection _headers;


        [SetUp]
        public void SetUp() {
            _httpContext = MockRepository.GenerateStub<HttpContextBase>();
            _httpRequest = MockRepository.GenerateStub<HttpRequestBase>();
            _httpContext.Stub(c => c.Request).Return(_httpRequest);
            _httpRequest.Stub(r => r.PathInfo).Return("");
            _form = new NameValueCollection();
            _httpRequest.Stub(r => r.Form).Return(_form);
            _acceptHeaderResponseTypeResolver = MockRepository.GenerateMock<IAcceptHeaderResponseTypeResolver>();
            _route = new RestfulRoute(URL, CONTROLLER, _acceptHeaderResponseTypeResolver);
            _httpRequest.Stub(r => r.HttpMethod).Return("Get");
            _headers = new NameValueCollection();
            _httpRequest.Stub(r => r.Headers).Return(_headers);

        }

        [Test]
        public void DefaultIsXml() {
            StubUrl();
            var routeData = GetRouteData();
            Assert.That(routeData.Values["responseType"], Is.EqualTo(Core.ResponseType.ResponseType.Xml));
        }

        [Test]
        public void HtmlResponseTypeIsPickedUpFromPath() {
            StubUrl(URL + ".html");
         
            var routeData = GetRouteData();
            Assert.That(routeData.Values["responseType"], Is.EqualTo(Core.ResponseType.ResponseType.Html));
        }

        [Test]
        public void JsonResponseTypeIsPickedUpFromPath() {
            StubUrl(URL + ".json");

            var routeData = GetRouteData();
            Assert.That(routeData.Values["responseType"], Is.EqualTo(Core.ResponseType.ResponseType.Json));
        }

        [Test]
        public void XmlResponseTypeIsPickedUpFromPath() {
            StubUrl(URL + ".xml");

            var routeData = GetRouteData();
            Assert.That(routeData.Values["responseType"], Is.EqualTo(Core.ResponseType.ResponseType.Xml));
        }

        [Test]
        public void JunkResponseTypeInPathIsIgnored() {
            StubUrl(URL + ".junk");

            var routeData = GetRouteData();
            Assert.That(routeData.Values["responseType"], Is.EqualTo(Core.ResponseType.ResponseType.Xml));
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
            _acceptHeaderResponseTypeResolver.Stub(r => r.Resolve(Arg<string>.Is.Anything)).Return(Core.ResponseType.ResponseType.Json);

            var routeData = GetRouteData();
            Assert.That(routeData.Values["responseType"], Is.EqualTo(Core.ResponseType.ResponseType.Json));
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