using System.Web.Mvc;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using SharpArch.Web.NHibernate;

namespace CarbonFitness.App.Web.Controllers {
	public class ProfileController : Controller {
		public ProfileController(IUserProfileBusinessLogic userProfileBusinessLogic, IUserContext userContext) {
			UserProfileBusinessLogic = userProfileBusinessLogic;
			UserContext = userContext;
		}

		public IUserProfileBusinessLogic UserProfileBusinessLogic { get; private set; }
		public IUserContext UserContext { get; private set; }

		[Authorize]
		public ActionResult Input() {
			var profileModel = new ProfileModel {IdealWeight = UserProfileBusinessLogic.GetIdealWeight(UserContext.User)};

			return View(profileModel);
		}


		[Authorize]
		[HttpPost]
		[Transaction]
		public ActionResult Input(ProfileModel profileModel) {
			if (ModelState.IsValid) {
				UserProfileBusinessLogic.SaveIdealWeight(UserContext.User, profileModel.IdealWeight);
			}
			return View(profileModel);
		}
	}
}