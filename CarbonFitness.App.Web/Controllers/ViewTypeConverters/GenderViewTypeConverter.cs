using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web.Controllers.ViewTypeConverters {
    public interface IGenderViewTypeConverter : IViewTypeConverter { }

    public class GenderViewTypeConverter : BaseViewTypeConverter, IGenderViewTypeConverter {
        public GenderViewTypeConverter(IUserProfileBusinessLogic userProfileBusinessLogic, IGenderTypeBusinessLogic genderTypeBusinessLogic) {
            UserProfileBusinessLogic = userProfileBusinessLogic;
            GenderTypeBusinessLogic = genderTypeBusinessLogic;
        }

        public IUserProfileBusinessLogic UserProfileBusinessLogic { get; private set; }
        public IGenderTypeBusinessLogic GenderTypeBusinessLogic { get; private set; }
        public IEnumerable<SelectListItem> GetViewTypes(User user) {
            return PopulateViewTypes(GenderTypeBusinessLogic.GetGenderTypes().ToArray(), UserProfileBusinessLogic.GetGender(user));
        }
    }
}