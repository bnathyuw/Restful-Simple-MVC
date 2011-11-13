using System.Net;
using NUnit.Framework;
using RestfulSimpleMvc.Core.StatusCodes;

namespace RestfulSimpleMvc.Unit.Tests.StatusCodes {
	[TestFixture]
	public class HtmlStatusCodeTranslatorTests {
		[Test, TestCaseSource("TestCaseSource")]
		public void Resource_status_maps_to_correct_http_status_code(ResourceStatus input, HttpStatusCode expectedOutput) {
			var statusCodeTranslator = new HtmlStatusCodeTranslator();
			var result = statusCodeTranslator.LookUp(input);
			Assert.That(result, Is.EqualTo(expectedOutput));
		}

		public object [] TestCaseSource {
			get {
				return new object[] {
				                    	new object[] {ResourceStatus.Created, HttpStatusCode.MovedPermanently},
										new object[] {ResourceStatus.Deleted, HttpStatusCode.MovedPermanently},
										new object[] {ResourceStatus.Moved, HttpStatusCode.MovedPermanently}
				                    };
			}
		}
	}
}