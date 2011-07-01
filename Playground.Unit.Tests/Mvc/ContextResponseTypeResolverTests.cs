using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using Playground.Web.Mvc;
using Rhino.Mocks;

namespace Playground.Unit.Tests.Mvc
{
	[TestFixture]
	public class ContextResponseTypeResolverTests
	{
		private IResponseTypeResolver _acceptHeaderResponseTypeResolver;
		private IResponseTypeResolver _routeDataResponseTypeResolver;
		private ContextResponseTypeResolver _responseTypeResolver;
		private ControllerContext _controllerContext;
		private HttpContextBase _httpContext;
		private HttpRequestBase _httpRequest;
		private RouteData _routeData;
		private NameValueCollection _headers;

		[SetUp]
		public void SetUp(){
			_controllerContext = MockRepository.GenerateStub<ControllerContext>();
			_httpContext = MockRepository.GenerateStub<HttpContextBase>();
			_httpRequest = MockRepository.GenerateStub<HttpRequestBase>();
			_controllerContext.HttpContext = _httpContext;
			_httpContext.Stub(c => c.Request).Return(_httpRequest);
			_routeData = new RouteData();
			_controllerContext.RouteData = _routeData;
			_headers = new NameValueCollection();
			_httpRequest.Stub(r => r.Headers).Return(_headers);

			_acceptHeaderResponseTypeResolver = MockRepository.GenerateStub<IResponseTypeResolver>();
			_routeDataResponseTypeResolver = MockRepository.GenerateStub<IResponseTypeResolver>();
			_responseTypeResolver = new ContextResponseTypeResolver(_routeDataResponseTypeResolver, _acceptHeaderResponseTypeResolver);
		}

		[Test]
		public void GetsDefaultXmlResponseTypeWhenNoDataAreSet()
		{
			var responseType = _responseTypeResolver.Resolve(_controllerContext);

			Assert.That(responseType, Is.EqualTo(ResponseType.Xml));
		}

		[Test]
		public void PassesRouteDataToResolverFirst() {
			const string routeValue = "routeValue";
			_routeData.Values.Add("responseType", routeValue);

			_responseTypeResolver.Resolve(_controllerContext);

			_routeDataResponseTypeResolver.AssertWasCalled(r => r.Resolve(routeValue));
		}

		[Test]
		public void ReturnsTypeFromRouteDataResolver() {
			_routeDataResponseTypeResolver.Stub(r => r.Resolve(Arg<string>.Is.Anything)).Return(ResponseType.Html);

			var responseType = _responseTypeResolver.Resolve(_controllerContext);

			Assert.That(responseType, Is.EqualTo(ResponseType.Html));
		}

		[Test]
		public void PassesAcceptHeaderToResolverIfRouteDataResolverReturnsNull() {
			const string acceptHeader = "acceptHeader";
			_routeDataResponseTypeResolver.Stub(r => r.Resolve(Arg<string>.Is.Anything)).Return(null);
			_headers.Add("Accept", acceptHeader);

			_responseTypeResolver.Resolve(_controllerContext);

			_acceptHeaderResponseTypeResolver.AssertWasCalled(r => r.Resolve(acceptHeader));
		}

		[Test]
		public void ReturnsTypeFromAcceptHeaderResolverIfRouteDataResolverReturnsNull() {
			const string acceptHeader = "acceptHeader";
			_routeDataResponseTypeResolver.Stub(r => r.Resolve(Arg<string>.Is.Anything)).Return(null);
			_acceptHeaderResponseTypeResolver.Stub(r => r.Resolve(Arg<string>.Is.Anything)).Return(ResponseType.Html);
			_headers.Add("Accept", acceptHeader);

			var responseType = _responseTypeResolver.Resolve(_controllerContext);

			Assert.That(responseType, Is.EqualTo(ResponseType.Html));
		}
		
		//[Test]
		//public void GetsCorrectXmlResponseTypeFromRoute() {
		//    var routeData = new RouteData();
		//    routeData.Values.Add("responseType", "xml");
		//    var fakeContext = GetFakeContext(routeData, new NameValueCollection());
		//    var responseTypeResolver = new ContextResponseTypeResolver();

		//    var responseType = responseTypeResolver.Resolve(fakeContext);
		//    Assert.That(responseType, Is.EqualTo(ResponseType.Xml));
		//}
		//[Test]
		//public void GetsCorrectHtmlResponseTypeFromRoute()
		//{
		//    var routeData = new RouteData();
		//    routeData.Values.Add("responseType", "html");
		//    var fakeContext = GetFakeContext(routeData, new NameValueCollection());
		//    var responseTypeResolver = new ContextResponseTypeResolver();

		//    var responseType = responseTypeResolver.Resolve(fakeContext); 
		//    Assert.That(responseType, Is.EqualTo(ResponseType.Html));
		//}
		//[Test]
		//public void GetsCorrectJsonResponseTypeFromRoute()
		//{
		//    var routeData = new RouteData();
		//    routeData.Values.Add("responseType", "json");
		//    var fakeContext = GetFakeContext(routeData, new NameValueCollection());
		//    var responseTypeResolver = new ContextResponseTypeResolver();

		//    var responseType = responseTypeResolver.Resolve(fakeContext);
		//    Assert.That(responseType, Is.EqualTo(ResponseType.Json));
		//}

		//[Test]
		//public void GetsCorrectXmlResponseTypeFromAcceptHeaders() {
		//    var headers = new NameValueCollection { { "Accept", "text/xml" } };
		//    var fakeContext = GetFakeContext(new RouteData(), headers);
		//    var responseTypeResolver = new ContextResponseTypeResolver();

		//    var responseType = responseTypeResolver.Resolve(fakeContext);
		//    Assert.That(responseType, Is.EqualTo(ResponseType.Xml));
		//}

		//[Test]
		//public void GetsCorrectHtmlResponseTypeFromAcceptHeaders()
		//{
		//    var headers = new NameValueCollection { { "Accept", "text/html" } };
		//    var fakeContext = GetFakeContext(new RouteData(), headers);
		//    var responseTypeResolver = new ContextResponseTypeResolver();

		//    var responseType = responseTypeResolver.Resolve(fakeContext);
		//    Assert.That(responseType, Is.EqualTo(ResponseType.Html));
		//}

		//[Test]
		//public void GetsCorrectJsonResponseTypeFromAcceptHeaders()
		//{
		//    var headers = new NameValueCollection { { "Accept", "application/json" } };
		//    var fakeContext = GetFakeContext(new RouteData(), headers);
		//    var responseTypeResolver = new ContextResponseTypeResolver();

		//    var responseType = responseTypeResolver.Resolve(fakeContext);
		//    Assert.That(responseType, Is.EqualTo(ResponseType.Json));
		//}

		//[Test]
		//public void GetsCorrectResponseTypeWhenHtmlAndXmlAccepted()
		//{
		//    var headers = new NameValueCollection { { "Accept", "text/html,text/xml" } };
		//    var fakeContext = GetFakeContext(new RouteData(), headers);
		//    var responseTypeResolver = new ContextResponseTypeResolver();

		//    var responseType = responseTypeResolver.Resolve(fakeContext);
		//    Assert.That(responseType, Is.EqualTo(ResponseType.Html));
		//}

		//[Test]
		//public void GetsCorrectResponseTypeWhenHtmlAndJsonAccepted()
		//{
		//    var headers = new NameValueCollection { { "Accept", "text/html, application/json" } };
		//    var fakeContext = GetFakeContext(new RouteData(), headers);
		//    var responseTypeResolver = new ContextResponseTypeResolver();

		//    var responseType = responseTypeResolver.Resolve(fakeContext);
		//    Assert.That(responseType, Is.EqualTo(ResponseType.Html));
		//}

		//[Test]
		//public void GetsCorrectResponseTypeWhenJsonAndXmlAccepted()
		//{
		//    var headers = new NameValueCollection { { "Accept", "text/xml ,application/json" } };
		//    var fakeContext = GetFakeContext(new RouteData(), headers);
		//    var responseTypeResolver = new ContextResponseTypeResolver();

		//    var responseType = responseTypeResolver.Resolve(fakeContext);
		//    Assert.That(responseType, Is.EqualTo(ResponseType.Json));
		//}

		//[Test]
		//public void GetsCorrectResponseTypeWhenHtmlAndJsonAndXmlAccepted()
		//{
		//    var headers = new NameValueCollection { { "Accept", "text/xml, text/html ,application/json" } };
		//    var fakeContext = GetFakeContext(new RouteData(), headers);
		//    var responseTypeResolver = new ContextResponseTypeResolver();

		//    var responseType = responseTypeResolver.Resolve(fakeContext);
		//    Assert.That(responseType, Is.EqualTo(ResponseType.Html));
		//}

		//[Test]
		//public void GetsCorrectResponseTypeWhenTypesArePrioritised()
		//{
		//    var headers = new NameValueCollection { { "Accept", "text/xml; q=.8,text/html;q =.7,application/json;q= .9" } };
		//    var fakeContext = GetFakeContext(new RouteData(), headers);
		//    var responseTypeResolver = new ContextResponseTypeResolver();

		//    var responseType = responseTypeResolver.Resolve(fakeContext);
		//    Assert.That(responseType, Is.EqualTo(ResponseType.Json));
		//}

		//[Test]
		//public void GetsCorrectResponseTypeWhenOneTypeIsPrioritised()
		//{
		//    var headers = new NameValueCollection { { "Accept", "text/xml,text/html;q=.7,application/json" } };
		//    var fakeContext = GetFakeContext(new RouteData(), headers);
		//    var responseTypeResolver = new ContextResponseTypeResolver();

		//    var responseType = responseTypeResolver.Resolve(fakeContext);
		//    Assert.That(responseType, Is.EqualTo(ResponseType.Json));
		//}
	}
}
