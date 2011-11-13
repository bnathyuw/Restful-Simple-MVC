using System.Net;
using NUnit.Framework;
using RestfulSimpleMvc.Core.StatusCodes;

namespace RestfulSimpleMvc.Unit.Tests.StatusCodes {
	[TestFixture]
	public class DefaultStatusCodeTranslatorTests {
		[Test, TestCaseSource("TestCaseSource")]
		public void Resource_status_maps_to_correct_http_status_code(ResourceStatus input, HttpStatusCode expectedOutput) {
			var statusCodeTranslator = new DefaultStatusCodeTranslator();
			var result = statusCodeTranslator.LookUp(input);
			Assert.That(result, Is.EqualTo(expectedOutput));
		}

		public object [] TestCaseSource {
			get {
				return new object[] {
				                    	new object[] {ResourceStatus.Created, HttpStatusCode.Created},
										new object[] {ResourceStatus.Deleted, HttpStatusCode.NoContent},
										new object[] {ResourceStatus.Moved, HttpStatusCode.MovedPermanently}
				                    };
			}
		}
	}
}