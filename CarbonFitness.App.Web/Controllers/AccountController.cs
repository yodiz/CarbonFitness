using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Routing;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;

namespace CarbonFitness.App.Web.Controllers {
	[HandleError]
	public class AccountController : Controller {
		// This constructor is not used by the MVC framework but is instead provided for ease
		// of unit testing this type. See the comments in AccountModels.cs for more information.
		public AccountController(IFormsAuthenticationService formsService, IMembershipBusinessLogic membershipBusinessLogic) {
			FormsService = formsService ?? new FormsAuthenticationService();
			MembershipBusinessLogic = membershipBusinessLogic;
		}

		public IFormsAuthenticationService FormsService { get; private set; }

		public IMembershipBusinessLogic MembershipBusinessLogic { get; private set; }

		protected override void Initialize(RequestContext requestContext) {
			if (requestContext.HttpContext.User.Identity is WindowsIdentity) {
				throw new InvalidOperationException("Windows authentication is not supported.");
			}

			base.Initialize(requestContext);
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext) {
			ViewData["PasswordLength"] = MembershipBusinessLogic.MinPasswordLength;

			base.OnActionExecuting(filterContext);
		}

		[Authorize]
		public ActionResult ChangePassword() {
			return View();
		}

		[Authorize]
		[HttpPost]
		public ActionResult ChangePassword(ChangePasswordModel model) {
			if (ModelState.IsValid) {
				if (MembershipBusinessLogic.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword)) {
					return RedirectToAction("ChangePasswordSuccess");
				}
				ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		[Authorize]
		public ActionResult ChangePasswordSuccess() {
			return View();
		}

		[Authorize]
		public ActionResult LogOff() {
			FormsService.SignOut();

			return RedirectToAction("Index", "Home");
		}

		public ActionResult LogOn() {
			return View();
		}

		[HttpPost]
		[SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
			Justification = "Needs to take same parameter type as Controller.Redirect()")]
		public ActionResult LogOn(LogOnModel model, string returnUrl) {
			if (ModelState.IsValid) {
				if (MembershipBusinessLogic.ValidateUser(model.UserName, model.Password)) {
					FormsService.SignIn(model.UserName, model.RememberMe);
					if (!String.IsNullOrEmpty(returnUrl)) {
						return Redirect(returnUrl);
					}
					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", "The user name or password provided is incorrect.");
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		public ActionResult Register() {
			return View();
		}
	}
}