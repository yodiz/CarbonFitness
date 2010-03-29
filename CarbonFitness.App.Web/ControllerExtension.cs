using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace CarbonFitness.App.Web {
	public static class ControllerExtension {
		public static void AddModelError<T>(this Controller controller, Expression<Func<T, DateTime>> namedPropertyToGet, string message) {
			var name = ExpressionHelper.GetExpressionText(namedPropertyToGet);
			addModelError(controller, name, message);
		}

		public static void AddModelError<T>(this Controller controller, Expression<Func<T, string>> namedPropertyToGet, string message) {
			var name = ExpressionHelper.GetExpressionText(namedPropertyToGet);
			addModelError(controller, name, message);
		}

		private static void addModelError(Controller controller, string name, string message) {
			controller.ModelState.AddModelError(name, message);
		}
	}
}