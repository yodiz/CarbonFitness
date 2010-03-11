using System.Web.Mvc;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller {
	public class BaseTestController {
		protected string getErrormessage<T>(T controller, string key) where T : System.Web.Mvc.Controller {
			ModelState modelState;
			controller.ModelState.TryGetValue(key, out modelState);
			Assert.That(modelState, Is.Not.Null);
			return modelState.Errors[0].ErrorMessage;
		}
	}
}