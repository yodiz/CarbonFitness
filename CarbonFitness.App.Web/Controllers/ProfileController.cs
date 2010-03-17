using System.Web.Mvc;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.Data.Model;
using Moq;
using SharpArch.Data.NHibernate;
using SharpArch.Web.NHibernate;

namespace CarbonFitness.App.Web.Controllers {
	public class ProfileController : Controller {

        public IUserProfileBusinessLogic UserProfileBusinessLogic { get; private set; }
        public IUserContext UserContext { get; private set; }

        public ProfileController(IUserProfileBusinessLogic userProfileBusinessLogic, IUserContext userContext) {
            this.UserProfileBusinessLogic = userProfileBusinessLogic;
            this.UserContext = userContext;
        }

        [Authorize]
		public ActionResult Input(){
            var profileModel = new ProfileModel();

            profileModel.IdealWeight = UserProfileBusinessLogic.GetIdealWeight(UserContext.User);

            return View(profileModel);
		}


        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult Input(ProfileModel profileModel)
        {
            UserProfileBusinessLogic.SaveIdealWeight(UserContext.User, profileModel.IdealWeight);

            return View(profileModel);
        }
    }
}