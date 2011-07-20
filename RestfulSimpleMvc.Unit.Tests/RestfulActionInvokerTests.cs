using System.Web.Mvc;
using NUnit.Framework;
using RestfulSimpleMvc.Core;
using Rhino.Mocks;

namespace RestfulSimpleMvc.Unit.Tests
{
	[TestFixture]
	public class RestfulActionInvokerTests
	{
		private ITypedResultFactory _typedResultFactory;
		private RestfulActionInvokerFacade _actionInvoker;

		[SetUp]
		public void SetUp()
		{
			_typedResultFactory = MockRepository.GenerateStub<ITypedResultFactory>();
			_actionInvoker = new RestfulActionInvokerFacade(_typedResultFactory);
		}

		[Test]
		public void CreateActionResultWithViewResultReturnsThatResult()
		{
			ControllerContext controllerContext = null;
			ActionDescriptor actionDescriptor = null;
			object actionReturnValue = new ViewResult();
			var actionResult = _actionInvoker.CreateActionResult(controllerContext, actionDescriptor, actionReturnValue);
			Assert.That(actionResult, Is.EqualTo(actionReturnValue));
		}

		[Test]
		public void CreateActionResultWithObjectReturnsSomethingElse()
		{
			ControllerContext controllerContext = null;
			var actionDescriptor = MockRepository.GenerateStub<ActionDescriptor>();
			object actionReturnValue = new {};
			var actionResult = _actionInvoker.CreateActionResult(controllerContext, actionDescriptor, actionReturnValue);
			Assert.That(actionResult, Is.Not.EqualTo(actionReturnValue));
		}
	}

	public class RestfulActionInvokerFacade : RestfulActionInvoker
	{
		public RestfulActionInvokerFacade(ITypedResultFactory typedResultFactory) : base(typedResultFactory) { }

		public new ActionResult CreateActionResult(ControllerContext controllerContext, ActionDescriptor actionDescriptor, object actionReturnValue)
		{
			return base.CreateActionResult(controllerContext, actionDescriptor, actionReturnValue);
		}

	}

}