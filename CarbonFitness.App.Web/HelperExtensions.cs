using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using ExpressionHelper=Microsoft.Web.Mvc.Internal.ExpressionHelper;

namespace CarbonFitness.App.Web {
	public static class HelperExtensions {
		public static string Action<TController>(this UrlHelper urlHelper, Expression<Action<TController>> action)
			where TController : Controller {
			var routeValues = ExpressionHelper.GetRouteValuesFromExpression(action);
			var url = urlHelper.Action(null, routeValues);
			return url;
		}
	}
}