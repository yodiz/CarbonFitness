using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using Moq;

namespace CarbonFitness.App.Web.Controllers {
	public class ProfileController : Controller {

        public IUserBusinessLogic UserBusinessLogic { get; private set; }
        public IUserContext UserContext { get; private set; }

        public ProfileController(IUserBusinessLogic userBusinessLogic, IUserContext userContext) {
            this.UserBusinessLogic = userBusinessLogic;
            this.UserContext = userContext;
        }

        [Authorize]
		public ActionResult Input(){
            var profileModel = new Models.ProfileModel();
            profileModel.IdealWeight = UserContext.User.Profile.IdealWeight;

            return View(profileModel);
		}


        [Authorize]
        [HttpPost]
        public ActionResult Input(Models.ProfileModel profileModel)
        {
            UserContext.User.Profile.IdealWeight = profileModel.IdealWeight;

            UserBusinessLogic.SaveOrUpdate(UserContext.User);

            return Input();
        }
    }
}