using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using CarbonFitness.App.Web.Controllers;
using NUnit.Framework;

namespace CarbonFitnessTest.Web {
	[TestFixture]
	public class AuthorizeTest {
		private static IEnumerable<Type> getControllerTypes(Assembly controllerAssmbly) {
			return controllerAssmbly.GetExportedTypes().Where(t => typeof(System.Web.Mvc.Controller).IsAssignableFrom(t)).ToList();
		}

		private static IEnumerable<MethodInfo> getActionMethods(IEnumerable<Type> controllerTypes) {
			return controllerTypes.SelectMany(t => t.GetMethods()).Where(m => typeof(ActionResult).IsAssignableFrom(m.ReturnType)).ToList();
		}

		private static bool HasAuthorizeAttribute(MethodInfo method) {
			return method.GetCustomAttributes(typeof(AuthorizeAttribute), true).Length > 0;
		}

		private static bool IsExcludedFromAuthorizeRule(MethodInfo method) {
			string action = method.DeclaringType.Name + "." + method.Name;
			switch (action) {
				case "UserController.Create":
				case "AccountController.LogOn":
				case "AccountController.Register":
				case "HomeController.Index":
				case "HomeController.About": 
				
				return true;
			}

			return false;
		}

		[Test]
		public void shouldHaveAuthorizeAttributeOnAllActions() {
			var controllerTypes = getControllerTypes(typeof(HomeController).Assembly);
			var actionMethods = getActionMethods(controllerTypes);

			foreach (var action in actionMethods) {
				if (IsExcludedFromAuthorizeRule(action)) continue;
				Assert.That(
					HasAuthorizeAttribute(action),
					"Action method " + action.DeclaringType.Name + "/" + action.Name + " doesn't have required AuthorizeAttribute"
					);
			}
		}
	}
}