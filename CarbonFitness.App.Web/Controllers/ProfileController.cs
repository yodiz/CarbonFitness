using System.Web.Mvc;
using CarbonFitness.App.Web.Controllers.ViewTypeConverters;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using SharpArch.Web.NHibernate;

namespace CarbonFitness.App.Web.Controllers {
    [HandleError]
    public class ProfileController : Controller {
        public ProfileController(IUserProfileBusinessLogic userProfileBusinessLogic, IGenderViewTypeConverter genderTypeConverter, IActivityLevelViewTypeConverter activityLevelViewTypeConverter, IUserContext userContext) {
            ActivityLevelViewTypeConverter = activityLevelViewTypeConverter;
            GenderTypeConverter = genderTypeConverter;
            UserProfileBusinessLogic = userProfileBusinessLogic;
            UserContext = userContext;
        }

        public IActivityLevelViewTypeConverter ActivityLevelViewTypeConverter { get; private set; }
        public IGenderViewTypeConverter GenderTypeConverter { get; private set; }
        public IUserProfileBusinessLogic UserProfileBusinessLogic { get; private set; }
        public IUserContext UserContext { get; private set; }

        [Authorize]
        public ActionResult Input() {
            return View(GetProfileModel());
        }


        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult Input(ProfileModel profileModel) {
            if (ModelState.IsValid) {
                UserProfileBusinessLogic.SaveProfile(UserContext.User, profileModel.IdealWeight, profileModel.Length, profileModel.Weight, profileModel.Age, profileModel.SelectedGender, profileModel.SelectedActivityLevel);
            }
            profileModel = GetProfileModel();
            return View(profileModel);
        }

        private ProfileModel GetProfileModel() {
            return new ProfileModel {
                IdealWeight = UserProfileBusinessLogic.GetIdealWeight(UserContext.User),
                Length = UserProfileBusinessLogic.GetLength(UserContext.User),
                Weight = UserProfileBusinessLogic.GetWeight(UserContext.User),
                BMI = UserProfileBusinessLogic.GetBMI(UserContext.User),
                Age = UserProfileBusinessLogic.GetAge(UserContext.User),
                GenderViewTypes = GenderTypeConverter.GetViewTypes(UserContext.User),
                ActivityLevelViewTypes = ActivityLevelViewTypeConverter.GetViewTypes(UserContext.User),
                BMR = UserProfileBusinessLogic.GetBMR(UserContext.User),
                DailyCalorieNeed = UserProfileBusinessLogic.GetDailyCalorieNeed(UserContext.User)
            };
        }
    }
}