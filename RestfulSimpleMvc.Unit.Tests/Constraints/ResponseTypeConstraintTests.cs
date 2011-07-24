using System.Web.Routing;
using NUnit.Framework;
using RestfulSimpleMvc.Core.Constraints;

namespace RestfulSimpleMvc.Unit.Tests.Constraints
{
    [TestFixture]
    public class ResponseTypeConstraintTests
    {
        [Test]
        public void AcceptsHtml()
        {
            var responseTypeConstraint = new ResponseTypeConstraint();
            var match = responseTypeConstraint.Match(null, null, "responseType", new RouteValueDictionary(new { responseType = "Html" }), RouteDirection.IncomingRequest);
            Assert.IsTrue(match);
        }

        [Test]
        public void AcceptsJson()
        {
            var responseTypeConstraint = new ResponseTypeConstraint();
            var match = responseTypeConstraint.Match(null, null, "responseType", new RouteValueDictionary(new { responseType = "Json" }), RouteDirection.IncomingRequest);
            Assert.IsTrue(match);
        }

        [Test]
        public void AcceptsXml()
        {
            var responseTypeConstraint = new ResponseTypeConstraint();
            var match = responseTypeConstraint.Match(null, null, "responseType", new RouteValueDictionary(new { responseType = "Xml" }), RouteDirection.IncomingRequest);
            Assert.IsTrue(match);
        }

        [Test]
        public void RejectsCrap()
        {
            var responseTypeConstraint = new ResponseTypeConstraint();
            var match = responseTypeConstraint.Match(null, null, "responseType", new RouteValueDictionary(new { responseType = "Crap" }), RouteDirection.IncomingRequest);
            Assert.IsFalse(match);
        }
    }
}