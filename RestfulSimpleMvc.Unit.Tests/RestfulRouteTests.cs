using System.Collections.Specialized;
using System.Web;
using System.Web.Routing;
using NUnit.Framework;
using RestfulSimpleMvc.Core;
using Rhino.Mocks;

namespace RestfulSimpleMvc.Unit.Tests
{
	[TestFixture]
	public class RestfulRouteTests
	{
		private HttpContextBase _httpContext;
		private HttpRequestBase _httpRequest;
		private RestfulRoute _route;
		private NameValueCollection _form;
		private const string CONTROLLER = "Methods";
		private const string URL = "Methods";

		[SetUp]
		public void SetUp() {
			_httpContext = MockRepository.GenerateStub<HttpContextBase>();
			_httpRequest = MockRepository.GenerateStub<HttpRequestBase>();
			_httpContext.Stub(c => c.Request).Return(_httpRequest);
			_httpRequest.Stub(r => r.AppRelativeCurrentExecutionFilePath).Return("~/" + URL);
			_httpRequest.Stub(r => r.PathInfo).Return("");
			_form = new NameValueCollection();
			_httpRequest.Stub(r => r.Form).Return(_form);
			_route = new RestfulRoute(URL, CONTROLLER);
		}

		[Test]
		public void GetCorrespondsToGetMethod() {
			_httpRequest.Stub(r => r.HttpMethod).Return("Get");
			var routeData = _route.GetRouteData(_httpContext);

			Assert.That(routeData != null);
			Assert.That(routeData.Values["action"], Is.EqualTo("Get"));
		}

		[Test]
		public void PutCorrespondsToPutMethod() {
			_httpRequest.Stub(r => r.HttpMethod).Return("Put");
			var routeData = _route.GetRouteData(_httpContext);

			Assert.That(routeData != null);
			Assert.That(routeData.Values["action"], Is.EqualTo("Put"));
		}

		[Test]
		public void PostCorrespondsToPostMethod() {
			_httpRequest.Stub(r => r.HttpMethod).Return("Post");
			var routeData = _route.GetRouteData(_httpContext);

			Assert.That(routeData != null);
			Assert.That(routeData.Values["action"], Is.EqualTo("Post"));
		}
		
		[Test]
		public void PostWithActionParamCorrespondsToPutMethod() {
			_httpRequest.Stub(r => r.HttpMethod).Return("Post");
			_form.Add("_action", "Put");
			var routeData = _route.GetRouteData(_httpContext);

			Assert.That(routeData != null);
			Assert.That(routeData.Values["action"], Is.EqualTo("Put"));
		}

		[Test]
		public void GetReturnsCorrectPath() {
			var requestContext = MockRepository.GenerateStrictMock<RequestContext>();
			var routeData = new RouteData();
			requestContext.Stub(r => r.RouteData).Return(routeData);
			requestContext.Stub(r => r.HttpContext).Return(_httpContext);
			var virtualPathData = _route.GetVirtualPath(requestContext, new RouteValueDictionary(new {controller = CONTROLLER, action = "Get"}));	
			Assert.That(virtualPathData != null);
			Assert.That(virtualPathData.VirtualPath, Is.EqualTo(URL));
		}

		[Test]
		public void PostReturnsCorrectPath() {
			var requestContext = MockRepository.GenerateStrictMock<RequestContext>();
			var routeData = new RouteData();
			requestContext.Stub(r => r.RouteData).Return(routeData);
			requestContext.Stub(r => r.HttpContext).Return(_httpContext);
			var virtualPathData = _route.GetVirtualPath(requestContext, new RouteValueDictionary(new { controller = CONTROLLER, action = "Post" }));
			Assert.That(virtualPathData != null);
			Assert.That(virtualPathData.VirtualPath, Is.EqualTo(URL));
		}

		[Test]
		public void PutReturnsCorrectPath() {
			var requestContext = MockRepository.GenerateStrictMock<RequestContext>();
			var routeData = new RouteData();
			requestContext.Stub(r => r.RouteData).Return(routeData);
			requestContext.Stub(r => r.HttpContext).Return(_httpContext);
			var virtualPathData = _route.GetVirtualPath(requestContext, new RouteValueDictionary(new { controller = CONTROLLER, action = "Put" }));
			Assert.That(virtualPathData != null);
			Assert.That(virtualPathData.VirtualPath, Is.EqualTo(URL));
		}

		[Test]
		public void OptionsReturnsCorrectPath() {
			var requestContext = MockRepository.GenerateStrictMock<RequestContext>();
			var routeData = new RouteData();
			requestContext.Stub(r => r.RouteData).Return(routeData);
			requestContext.Stub(r => r.HttpContext).Return(_httpContext);
			var virtualPathData = _route.GetVirtualPath(requestContext, new RouteValueDictionary(new { controller = CONTROLLER, action = "Options" }));
			Assert.That(virtualPathData != null);
			Assert.That(virtualPathData.VirtualPath, Is.EqualTo(URL));
		}
	}
}