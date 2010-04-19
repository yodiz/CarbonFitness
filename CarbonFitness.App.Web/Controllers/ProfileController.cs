using System.Collections.Generic;
using System.Web.Mvc;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using SharpArch.Web.NHibernate;

namespace CarbonFitness.App.Web.Controllers {
    [HandleError]
    public class ProfileController : Controller {
        public ProfileController(IUserProfileBusinessLogic userProfileBusinessLogic, IGenderTypeBusinessLogic genderTypeBusinessLogic, IUserContext userContext) {
            GenderTypeBusinessLogic = genderTypeBusinessLogic;
            UserProfileBusinessLogic = userProfileBusinessLogic;
            UserContext = userContext;
        }

        public IGenderTypeBusinessLogic GenderTypeBusinessLogic { get; private set; }
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
                UserProfileBusinessLogic.SaveProfile(UserContext.User, profileModel.IdealWeight, profileModel.Length, profileModel.Weight, profileModel.SelectedGender);
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
                GenderViewTypes = GetGenderViewTypes()
            };
        }

        private IEnumerable<SelectListItem> GetGenderViewTypes() {
            return PopulateGenderViewTypes(GenderTypeBusinessLogic.GetGenderTypes(), UserProfileBusinessLogic.GetGender(UserContext.User));
        }

        private IEnumerable<SelectListItem> PopulateGenderViewTypes(IEnumerable<GenderType> genderTypes, GenderType selectedGender) {
            var result = new List<SelectListItem>();
            foreach (GenderType genderType in genderTypes) {
                result.Add(new SelectListItem {Text = genderType.Name, Value = genderType.Id.ToString(), Selected = selectedGender.Name == genderType.Name});
            }
            return result;
        }
    }
}