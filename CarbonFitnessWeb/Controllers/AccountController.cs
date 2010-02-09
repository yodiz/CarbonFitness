using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Builder;
using CarbonFitness;
using CarbonFitness.Repository;
using CarbonFitnessWeb.Models;

namespace CarbonFitnessWeb.Controllers {
	[HandleError]
	public class AccountController : Controller {
		// This constructor is used by the MVC framework to instantiate the controller using
		// the default forms authentication and membership providers.
		public AccountController()
			: this(null, null) {

			var c = ComponentBuilder.Current;
			MembershipService = c.Resolve<IMembershipService>();
		}

		// This constructor is not used by the MVC framework but is instead provided for ease
		// of unit testing this type. See the comments in AccountModels.cs for more information.
		public AccountController(IFormsAuthenticationService formsService, IMembershipService membershipService) {
			FormsService = formsService ?? new FormsAuthenticationService();
			MembershipService = membershipService;
		}

		public IFormsAuthenticationService FormsService { get; private set; }

		public IMembershipService MembershipService { get; private set; }

		protected override void Initialize(RequestContext requestContext) {
			if (requestContext.HttpContext.User.Identity is WindowsIdentity) {
				throw new InvalidOperationException("Windows authentication is not supported.");
			} else {
				base.Initialize(requestContext);
			}
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext) {
			ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

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
				if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword)) {
					return RedirectToAction("ChangePasswordSuccess");
				} else {
					ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		public ActionResult ChangePasswordSuccess() {
			return View();
		}

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
				if (MembershipService.ValidateUser(model.UserName, model.Password)) {
					FormsService.SignIn(model.UserName, model.RememberMe);
					if (!String.IsNullOrEmpty(returnUrl)) {
						return Redirect(returnUrl);
					} else {
						return RedirectToAction("Index", "Home");
					}
				} else {
					ModelState.AddModelError("", "The user name or password provided is incorrect.");
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		public ActionResult Register() {
			return View();
		}
	}
}